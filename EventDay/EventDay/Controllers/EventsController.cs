using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventDay.Models;
using System.IO;
using System.Web.Security;
using PagedList;
using System.Text.RegularExpressions;

namespace EventDay.Controllers
{
    public class EventsController : Controller
    {
        private EventContext db = new EventContext();

        //
        // GET: /Events/

        public ViewResult Index(string sortOrder, string search, string currentFilter, string eventCategory, int? page)
        {
            var categories = db.Category.Select(c => c.Name).Distinct().ToList();
            ViewBag.CurrentSort = sortOrder;
          //  ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.DateSortParm = sortOrder == "DateCreated" ? "datecreated_desc" : "DateCreated";
            
            
            ViewBag.eventCategory = new SelectList(categories); //lista do dropdown        

            if (search != null)
            {
                page = 1;
            }
            else
            {
                search = currentFilter;
            }
            
            ViewBag.CurrentFilter = search;
            
            
            //var books = from s in db.Books
            //  select s;
            var events = db.Event.Select(b => b).Where(e => e.DateEnd >= DateTime.Today).Where(e => String.Compare(e.AccessId,"Widoczne",false) == 0);


            if (!String.IsNullOrEmpty(search))
            {
                events = events.Where(b => b.City.Contains(search));
            }

            if (!string.IsNullOrEmpty(eventCategory))
            {
                events = events.Where(b => b.CategoryId == (db.Category.Where(s => s.Name.Equals(eventCategory)).Select(c => c.CategoryId)).FirstOrDefault());
            }

            switch (sortOrder)
            {
                case "DateCreated":
                    events = events.OrderBy(s => s.DateCreated);
                    break;
                case "datecreated_desc":
                    events = events.OrderByDescending(s => s.DateCreated);
                    break;
                default:  // Name ascending 
                    events = events.OrderByDescending(s => s.DateCreated);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
           
            return View(events.ToPagedList(pageNumber, pageSize));
           // return View(events);
        }

        //GET: /Books/Details
        public ActionResult Details(int id)
        {
            //jesli przekazano jakis blad z dolaczania
            if (TempData["ErrorMessage"]!=null) ViewBag.ErrorMessage = TempData["ErrorMessage"].ToString();

            var mevent = db.Event.Where(e => e.EventId == id).First();
            var comments = db.Comment.Where(c => c.EventId.Equals(id)).ToList();
            
            ViewBag.FreeSeats = mevent.Capacity-db.JoinEvent.Where(c => c.EventId.Equals(id)).Count();

            //**//
            DateTime data = Convert.ToDateTime(mevent.HourBegin);
            mevent.HourBegin = data.ToString("HH:mm");

            data = Convert.ToDateTime(mevent.HourEnd);
            mevent.HourEnd = data.ToString("HH:mm");

            data = Convert.ToDateTime(mevent.HourBeginRegistration);
            mevent.HourBeginRegistration = data.ToString("HH:mm");

            data = Convert.ToDateTime(mevent.HourEndRegistration);
            mevent.HourEndRegistration = data.ToString("HH:mm");
            //**//

            ViewBag.Gallery = db.Image.Where(b => b.EventId == id);//(from f in db.Image where f.EventId == id select f.Url).ToList();

            return View(new EventHelper { EventId = id, Event = mevent, Comments = comments });

        }

        //
        // GET: /Events/Create
        public ActionResult Create()
        {
            ViewBag.ViowodeshipList = CreateViowodeshipList();
            ViewBag.eventCategory = new SelectList(db.Category, "CategoryId", "Name");

            return View();
        }


        //
        // POST: /Events/Create
        [HttpPost]
        public ActionResult Create(Event e, HttpPostedFileBase fileRegulations, HttpPostedFileBase fileProfileImage)
        {
            e.DateCreated = DateTime.Now;
            e.Username = User.Identity.Name;
           // e.Locality = "domyslna";

            if (ModelState.IsValid)
            {
                string dateCreated = e.DateCreated.ToString().Replace(" ", "").Replace(":", "").Replace("-", "");

                if (fileRegulations != null && fileRegulations.ContentLength > 0)
                {
                    //nazwa plitu == username + data + R dla regulations lub P dla ProfileImage + nazwa pliku;
                    string fileName = e.Username + dateCreated + "R" + Path.GetFileName(fileRegulations.FileName);
                    string path = Path.Combine(Server.MapPath("~/Content/Uploads"), fileName);
                    fileRegulations.SaveAs(path);

                    e.Regulations = fileName;
                }

                if (fileProfileImage != null && fileProfileImage.ContentLength > 0)
                {
                    string fileName = e.Username + dateCreated + "P" + Path.GetFileName(fileProfileImage.FileName);
                    string path = Path.Combine(Server.MapPath("~/Content/Uploads"), fileName);
                    fileProfileImage.SaveAs(path);

                    e.ProfileImage = fileName;
                }


                db.Event.Add(e);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.eventCategory = new SelectList(db.Category, "CategoryId", "Name", e.CategoryId);
            return View(e);
        }

        //
        // GET: /BeadMenager/Edit/5

        public ActionResult Edit(int id)
        {
            Event e = db.Event.Find(id);

            ViewBag.CategoryId = new SelectList(db.Category, "CategoryId", "Name", e.CategoryId);

            return View(e);
        }

        //
        // POST: /BeadMenager/Edit/5

        [HttpPost]
        public ActionResult Edit(Event e)
        {
            //Event ev = db.Event.Find(e.EventId);

            if (ModelState.IsValid)
            {
                db.Entry(e).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Category, "CategoryId", "Name", e.CategoryId);

            return View(e);
        }

        [Authorize]
        public ActionResult JoinEvent(int id)
        {
            Event mEvent = db.Event.Find(id);
            if (mEvent == null) return HttpNotFound();

            if (String.Compare(mEvent.Username,User.Identity.Name,true) == 0) return RedirectToAction("Details", "Events", new { id = id });

            //sprawdzenie daty rejestracji
            if (DateTime.Compare(DateTime.Now.Date, mEvent.DateBeginRegistation.Date) <= 0 || DateTime.Compare(DateTime.Now.Date,mEvent.DateEndRegistation.Date) >= 0)
            {
                /*string timeRegex = "\\d{1,2}:\\d{2}:\\d{2}";
                var begin = Regex.Match(mEvent.HourBeginRegistration,timeRegex);
                var end = Regex.Match(mEvent.HourEndRegistration,timeRegex);
                var beginGroups = Regex.Match(begin.ToString(),"\\d{1,2}:");
                var endGroups = Regex.Match(end.ToString(), "\\d{1,2}:");
                var beginH = beginGroups.Groups[0].Value;
                var beginM = beginGroups.Groups[1].Value;
                var endH = endGroups.Groups[0].Value;
                var endM = endGroups.Groups[1].Value;*/
                DateTime begin = DateTime.Parse(mEvent.HourBeginRegistration);
                DateTime end = DateTime.Parse(mEvent.HourEndRegistration);
                //spr. czasu rejestracji
                //System.Diagnostics.Debug.WriteLine("begin: "+beginGroups.Groups[1]); //"beginH:" + beginH + " beginM:" + beginM + " endH:" + endH + " endM:" + endM
                System.Diagnostics.Debug.WriteLine(Int32.Parse(DateTime.Now.Hour.ToString()) <= Int32.Parse(begin.Hour.ToString()) && Int32.Parse(DateTime.Now.Minute.ToString()) <= Int32.Parse(begin.Minute.ToString()));
                if (Int32.Parse(DateTime.Now.Hour.ToString()) <= Int32.Parse(begin.Hour.ToString()) || (Int32.Parse(DateTime.Now.Hour.ToString()) <= Int32.Parse(begin.Hour.ToString()) && Int32.Parse(DateTime.Now.Minute.ToString()) <= Int32.Parse(begin.Minute.ToString())) || Int32.Parse(DateTime.Now.Hour.ToString()) > Int32.Parse(end.Hour.ToString()) || (Int32.Parse(DateTime.Now.Hour.ToString()) > Int32.Parse(end.Hour.ToString()) && Int32.Parse(DateTime.Now.Minute.ToString()) > Int32.Parse(end.Minute.ToString())))
                {
                    TempData["ErrorMessage"] = "Nie możesz dołączyć do wydarzenia - rejestracja jest zamknięta.";
                    return RedirectToAction("Details", "Events", new { id = id });
                }
            }

            var joinedUserEvent = db.JoinEvent.Include(e => e.Event).Where(u => u.Username == User.Identity.Name).Where(e => e.EventId == id).ToList();
            var joinedFoundEvents = db.JoinEvent.Where(e => e.EventId == id).ToList();

            if (joinedFoundEvents.Count() < mEvent.Capacity && joinedUserEvent.Count == 0)
            {
                JoinEvent join = new JoinEvent();
                join.EventId = mEvent.EventId;
                join.Username = User.Identity.Name;
                join.JoinDate = DateTime.Now;
                join.Status = 0;

                db.JoinEvent.Add(join);
                db.SaveChanges();
            }else TempData["ErrorMessage"] = "Nie możesz dołączyć do wydarzenia - nie ma już miejsc albo uczestniczysz już w tym wydarzeniu.";

            return RedirectToAction("Details", "Events", new { id = id });
        }


        public ActionResult ShowGuests(int id)
        {
            Event mEvent = db.Event.Find(id);
            if (mEvent == null) return HttpNotFound();

            List<JoinEvent> joined = db.JoinEvent.Where(e => e.EventId == id).ToList();
            if (joined == null) return HttpNotFound();

            return View(joined);
        }


        public ActionResult RemoveFromEvent(int idJoin, int idEvent)
        {
            JoinEvent join = db.JoinEvent.Find(idJoin);
            if (join == null) return HttpNotFound();

            db.JoinEvent.Remove(join);
            db.SaveChanges();

            return RedirectToAction("ShowGuests", new { id = idEvent });
        }

        public ActionResult UserDetails(string name)
        {
            return RedirectToAction("Details", "UserProfile", new { name = name });
        }


        //////////// OTHER     ///////////////////////////////////

        public List<SelectListItem> CreateViowodeshipList()
        {
            List<SelectListItem> Viowodeship = new List<SelectListItem>();

            Viowodeship.Add(new SelectListItem { Text = "dolnośląskie", Value = "dolnośląskie" });
            Viowodeship.Add(new SelectListItem { Text = "kujawsko-pomorskie", Value = "kujawsko-pomorskie" });
            Viowodeship.Add(new SelectListItem { Text = "lubelskie", Value = "lubelskie" });
            Viowodeship.Add(new SelectListItem { Text = "lubuskie", Value = "lubuskie" });
            Viowodeship.Add(new SelectListItem { Text = "łódzkie", Value = "łódzkie" });
            Viowodeship.Add(new SelectListItem { Text = "małopolskie", Value = "małopolskie" });
            Viowodeship.Add(new SelectListItem { Text = "mazowieckie", Value = "mazowieckie" });
            Viowodeship.Add(new SelectListItem { Text = "opolskie", Value = "opolskie" });
            Viowodeship.Add(new SelectListItem { Text = "podkarpackie", Value = "podkarpackie" });
            Viowodeship.Add(new SelectListItem { Text = "podlaskie", Value = "podlaskie" });
            Viowodeship.Add(new SelectListItem { Text = "pomorskie", Value = "pomorskie", Selected = true });
            Viowodeship.Add(new SelectListItem { Text = "śląskie", Value = "śląskie" });
            Viowodeship.Add(new SelectListItem { Text = "świętokrzyskie", Value = "świętokrzyskie" });
            Viowodeship.Add(new SelectListItem { Text = "warmińsko-mazurskie", Value = "warmińsko-mazurskie" });
            Viowodeship.Add(new SelectListItem { Text = "wielkopolskie", Value = "wielkopolskie" });
            Viowodeship.Add(new SelectListItem { Text = "zachodniopomorskie", Value = "zachodniopomorskie" });

            //ViewBag.ViowodeshipList = Viowodeship;
            return Viowodeship;
        }

        //////////// IMAGE CRUD ///////////////////////////////////////

    }
}