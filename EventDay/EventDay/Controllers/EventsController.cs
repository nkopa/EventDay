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
            //var books = from s in db.Books
            //  select s;
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

         //GET: /Books/Details
        public ActionResult Details(int id)
        {

            var mevent = db.Event.Where(e => e.EventId == id).First();
            var comments = db.Comment.Where(c => c.EventId.Equals(id)).ToList();
            return View(new EventHelper {EventId=id, Event = mevent, Comments = comments});
  
        }

        //
        // GET: /Events/Create
        public ActionResult Create()
        {

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
    }

    //////////// IMAGE CRUD ///////////////////////////////////////


}