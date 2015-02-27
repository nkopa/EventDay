using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventDay.Models;

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

    }
}