using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using EventDay.Models;

namespace EventDay.Controllers
{ 
    [Authorize]
    public class YourEventsController : Controller
    {
        private EventContext db = new EventContext();

        //
        // GET: /Events/

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
                ViewBag.Info = "Eventy, w których uczestniczysz";
                ViewBag.Type = "joined";
                //mEvent = db.Event.Include(e => e.Category).Where(u => u.Username == User.Identity.Name).ToList();
            }
            else
            {
                mEvent = db.Event.Include(e => e.Category).Where(u => u.Username == User.Identity.Name).ToList();
                ViewBag.Title = "Twoje eventy - stworzone";
                ViewBag.Info = "Eventy utworzone";
                ViewBag.Type = "created";
            }

            return View(mEvent);
        }

        //
        // GET: /Events/Details/5

        public ActionResult Details(int id)
        {
            return RedirectToAction("Details", "Events", new { id = id });
            //Event mEvent = db.Event.Find(id);
            //return View(mEvent);
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
            //e.Locality = "domyslna";

            ////konwertowanie daty                     
            e.DateBegin = DateSplit(e.HourBegin, "d", "B");
            e.DateBeginRegistation = DateSplit(e.HourBeginRegistration, "d", "B");
            e.DateEnd = DateSplit(e.HourBegin, "D", "E");
            e.DateEndRegistation = DateSplit(e.HourBeginRegistration, "D", "E");
            e.HourEnd = DateSplit(e.HourBegin, "H", "E").ToString();
            e.HourEndRegistration = DateSplit(e.HourBeginRegistration, "H", "E").ToString();

            e.HourBegin = DateSplit(e.HourBegin, "H", "B").ToString();
            e.HourBeginRegistration = DateSplit(e.HourBeginRegistration, "H", "B").ToString();

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
            //wartosci domyslne, musza byc bo sie wtedy details widok rozwala
           if (e.ContactNumber == null) e.ContactNumber = "brak";
           if (e.ContactEmail == null) e.ContactEmail = "brak";
           if (e.Website == null) e.Website = "brak";
           if (e.Street == null) e.Street = "brak";
           if (e.HouseNumber == null) e.HouseNumber = "brak";
           if (e.ApartmentNumber == null) e.ApartmentNumber = "brak";
           if (e.Directions == null) e.Directions = "brak";
           if (e.Regulations == null) e.Regulations = "brak";
            ////aktualizowanie bazy
            db.Event.Add(e);
            db.SaveChanges();

            return RedirectToAction("Index");

            //ViewBag.eventCategory = new SelectList(db.Category, "CategoryId", "Name", e.CategoryId);
            //return View(e);
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

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        //inne
        public ActionResult LeafeEvent(int id)
        {
            Event mEvent = db.Event.Find(id);
            if (mEvent == null) return HttpNotFound();

            var joinedFindedEvent = db.JoinEvent.Include(e => e.Event).Where(u => u.Username == User.Identity.Name).Where(e => e.EventId == id).ToList();
            if (joinedFindedEvent.Count == 0) return HttpNotFound();

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

            if (Regex.IsMatch(toSplit, @"^\d{2}:\d{2}\s+\d{1,2}/\d{1,2}/\d{4}\s*-\s*\d{2}:\d{2}\s+\d{1,2}/\d{1,2}/\d{4}\s*$"))
            {
                //toSplit == "19:26 10/03/2015 - 19:26 18/03/2015"
                //cutDate == [0]=19, [1]=26, [2]=10, [3]=03, [4]=2015, [5]=19, [6]=26, [7]=18, [8]=03, [9]=2015
             
                string[] split = toSplit.Split(new Char[] { ' ', '-', ':', '/'});
                string[] cutDate = new String[10];
                int i = 0;

                foreach (string s in split)
                {
                    if (s.Trim() != "")
                    {
                        cutDate[i] = s.Trim();
                        i++;
                    }
                }

                if (Regex.IsMatch(HourDate + BeginEnd, @"[TtHh0(Hour)]{1}[Bb0(Begin)]{1}")) // HourBegin
                {
                    return new DateTime(0001, 01, 01, Convert.ToInt32(cutDate[0]), Convert.ToInt32(cutDate[1]), 00);
                }
                if (Regex.IsMatch(HourDate + BeginEnd, @"[TtHh0(Hour)]{1}[Ee1(End)]{1}")) // HourEnd 
                {
                    return new DateTime(0001, 01, 01, Convert.ToInt32(cutDate[5]), Convert.ToInt32(cutDate[6]), 00); 
                }
                if (Regex.IsMatch(HourDate + BeginEnd, @"[Dd1(Date)]{1}[Bb0(Begin)]{1}")) // DateBegin
                {
                    return new DateTime(Convert.ToInt32(cutDate[4]), Convert.ToInt32(cutDate[3]), Convert.ToInt32(cutDate[2]), 00, 00, 00);
                }
                if (Regex.IsMatch(HourDate + BeginEnd, @"[Dd1(Date)]{1}[Ee1(End)]{1}")) // DateEnd
                {
                    return new DateTime(Convert.ToInt32(cutDate[9]), Convert.ToInt32(cutDate[8]), Convert.ToInt32(cutDate[7]), 00, 00, 00);
                }
            }
             
            //gdy toSplit nie jest poprawnym opisem daty i czasu          
            return new DateTime(0001, 01, 01, 00, 00, 00);
        }

        //////////// IMAGE CRUD ///////////////////////////////////////

    }
}