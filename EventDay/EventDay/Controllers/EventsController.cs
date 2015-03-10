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

namespace EventDay.Controllers
{ 
    public class EventsController : Controller
    {
        private EventContext db = new EventContext();

        //
        // GET: /Events/

        public ViewResult Index(string search, string eventCategory)
        {
            var categories = db.Category.Select(c => c.Name).Distinct().ToList();

            ViewBag.eventCategory = new SelectList(categories); //lista do dropdown        

            //var books = from s in db.Books
            //  select s;
            var events = db.Event.Select(b => b).Where(e => e.DateEnd >= DateTime.Today);


           //if (!String.IsNullOrEmpty(search))
           // {
              //  events = events.Where(b => b.Title.Contains(search));
           // }

            if (!string.IsNullOrEmpty(eventCategory))
            {
                events = events.Where(b => b.CategoryId == (db.Category.Where(s => s.Name.Equals(eventCategory)).Select(c => c.CategoryId)).FirstOrDefault());

            }


            return View(events);
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

            var categories = new SelectList(db.Category, "CategoryId", "Name");
            ViewBag.eventCategory = categories; //lista do dropdown

            return View();
        }

      
        //
        // POST: /Events/Create
        [HttpPost]
        public ActionResult Create(Event e, HttpPostedFileBase fileRegulations, HttpPostedFileBase fileProfileImage)
        {
            e.DateCreated = DateTime.Now;
            e.Username = User.Identity.Name;

            string dateCreated = e.DateCreated.ToString().Replace(" ", "").Replace(":", "").Replace("-", "");

            if (fileProfileImage != null && fileRegulations.ContentLength > 0)
            {     
                //nazwa plitu == username + data + R dla regulations lub P dla ProfileImage + nazwa pliku;
                string fileName = e.Username + dateCreated + "R" + Path.GetFileName(fileRegulations.FileName);
                string path = Path.Combine(Server.MapPath("~/Content/Uploads"), fileName);
                fileRegulations.SaveAs(path);

                e.Regulations = fileName;
            }
            

            if (fileProfileImage != null && fileProfileImage.ContentLength > 0)
            {
                string fileName = e.Username + dateCreated + "P" + Path.GetFileName(fileRegulations.FileName);
                string path = Path.Combine(Server.MapPath("~/Content/Uploads"), fileName);
                fileProfileImage.SaveAs(path);

                e.ProfileImage = fileName;
            }
            
           
                       if (ModelState.IsValid)
                        {
                            db.Event.Add(e);
                            db.SaveChanges();                
                        }
             
            return RedirectToAction("Index");
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
}