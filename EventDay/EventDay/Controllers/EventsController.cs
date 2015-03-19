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
            //ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.DateSortParm = sortOrder == "DateEnd" ? "dateend_desc" : "DateEnd";
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
            

            var events = db.Event.Select(b => b).Where(e => e.DateEnd >= DateTime.Today);


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
                case "DateEnd":
                    events = events.OrderBy(s => s.DateEnd);
                    break;
                case "DateEnd_desc":
                    events = events.OrderByDescending(s => s.DateEnd);
                    break;
                default:  // Name ascending 
                    events = events.OrderBy(s => s.Title);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(events.ToPagedList(pageNumber, pageSize));
        }
/*
 * // YourEventsController/Index()
        public ViewResult Index(string searching = "created")
        {
            IList<Event> mEvent = null;
            if (String.Compare(searching, "joined", false) == 0)
            {
                var joined = db.JoinEvent.Include(e => e.Event).Where(u => u.Username == User.Identity.Name).ToList();

                mEvent = new List<Event>();
                foreach (JoinEvent joinedEvent in joined)
                {
                    mEvent.Add(joinedEvent.Event);
                }

                ViewBag.Title = "Twoje eventy - dołączyłaś/eś";
                ViewBag.Type = "joined";
                //mEvent = db.Event.Include(e => e.Category).Where(u => u.Username == User.Identity.Name).ToList();
            }
            else
            {
                mEvent = db.Event.Include(e => e.Category).Where(u => u.Username == User.Identity.Name).ToList();
                ViewBag.Title = "Twoje eventy - stworzone";
                ViewBag.Type = "created";
            }

            return View(mEvent);
        }
*/

        //GET: /Books/Details
        public ActionResult Details(int id)
        {

            var mevent = db.Event.Where(e => e.EventId == id).First();
            var comments = db.Comment.Where(c => c.EventId.Equals(id)).ToList();
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
            e.Locality = "domyslna";
            
            ////konwertowanie daty
            /*e.HourBegin = DateSplit(e.HourBegin, "H","B").ToString();
            e.HourBeginRegistration = DateSplit(e.HourBeginRegistration, "H","B").ToString();
            e.HourEnd = DateSplit(e.HourBegin, "H","E").ToString();           
            e.HourEndRegistration = DateSplit(e.HourBeginRegistration, "H", "E").ToString();
            

            e.DateBegin = DateSplit(e.HourBegin, "d","B");
            e.DateBeginRegistation = DateSplit(e.HourBeginRegistration, "d", "B");
            e.DateEnd = DateSplit(e.HourBegin, "d", "e");
            e.DateEndRegistation = DateSplit(e.HourBeginRegistration, "d", "e");
            */
            e.DateBegin = DateTime.Now;
            e.DateBeginRegistation = DateTime.Now;
            e.DateEnd = DateTime.Now;
            e.DateEndRegistation = DateTime.Now;

            e.HourEnd = e.HourBegin;
            e.HourEnd = e.HourBeginRegistration;

            e.Voivoweship = "asdfasdf";
            ////Ładowanie plików
            //nazwa plitu == username + DateCreated + R dla regulations lub P dla ProfileImage + nazwa pliku;

                string dateCreated = e.DateCreated.ToString().Replace(" ", "").Replace(":", "").Replace("-", "");

                if (fileRegulations != null && fileRegulations.ContentLength > 0)
                {
                   
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

            ////aktualizowanie bazy
                if (ModelState.IsValid)
                    {
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

        //
        // GET: /Events/Delete/5

        public ActionResult Delete(int id)
        {
            Event mEvent = db.Event.Find(id);
            return View(mEvent);
        }

        //
        // POST: /Events/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Event mEvent = db.Event.Find(id);
            db.Event.Remove(mEvent);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


/// ///////////////////////////////////////////////////////

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


        public ActionResult JoinEvent(int id)
        {
            Event mEvent = db.Event.Find(id);
            if (mEvent == null) return HttpNotFound();

            JoinEvent join = new JoinEvent();
            join.EventId = mEvent.EventId;
            join.Username = User.Identity.Name;
            join.JoinDate = DateTime.Now;
            join.Status = 0;

            db.JoinEvent.Add(join);
            db.SaveChanges();

            return RedirectToAction("Details", "Events", new { id = id });
        }

        public ActionResult LeafeEvent(int id)
        {
            Event mEvent = db.Event.Find(id);
            if (mEvent == null) return HttpNotFound();

            var joinedFindedEvent = db.JoinEvent.Include(e => e.Event).Where(u => u.Username == User.Identity.Name).Where(e => e.Event.EventId == id).ToList();
            if (joinedFindedEvent.Count != 1) return HttpNotFound();

            db.JoinEvent.Remove(joinedFindedEvent[0]);
            db.SaveChanges();
            return RedirectToAction("Index", new { searching = "joined" });
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

        public static DateTime DateSplit(string toSplit, string HourDate, string BeginEnd)
        {

            //toSplit == "19:26 10/03/2015 - 19:26 18/03/2015" 
            string[] split = toSplit.Split(new Char[] { ' ', '-' });

            if (Regex.IsMatch(HourDate + BeginEnd, @"[TtHh0(Hour)]{1}[Bb0(Begin)]{1}"))
            {
                return Convert.ToDateTime("0001/01/01 " + split[0].Trim()); // HourBegin
            }
            if (Regex.IsMatch(HourDate + BeginEnd, @"[TtHh0(Hour)]{1}[Ee1(End)]{1}"))
            {
                return Convert.ToDateTime("0001/01/01 " + split[4].Trim()); // HourEnd 
            }
            if (Regex.IsMatch(HourDate + BeginEnd, @"[Dd1(Date)]{1}[Bb0(Begin)]{1}"))
            {
                string[] split2 = split[1].Split(new Char[] { '/' });
                return new DateTime(Convert.ToInt32(split2[2]), Convert.ToInt32(split2[1]), Convert.ToInt32(split2[0]), 00, 00, 00);
            }
            if (Regex.IsMatch(HourDate + BeginEnd, @"[Dd1(Date)]{1}[Ee1(End)]{1}"))
            {
                string[] split2 = split[5].Split(new Char[] { '/' });
                return new DateTime(Convert.ToInt32(split2[2]), Convert.ToInt32(split2[1]), Convert.ToInt32(split2[0]), 00, 00, 00);
            }
            return new DateTime(0001, 01, 01, 00, 00, 00);
        }

        //////////// IMAGE CRUD ///////////////////////////////////////

    }
}