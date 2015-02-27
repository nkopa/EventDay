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
    [Authorize]
    public class YourEventsController : Controller
    {
        private EventContext db = new EventContext();

        //
        // GET: /Events/

        public ViewResult Index()
        {
            var mEvent = db.Event.Include(e => e.Category).Where(u => u.Username == User.Identity.Name);
            return View(mEvent.ToList());
        }

        //
        // GET: /Events/Details/5

        public ViewResult Details(int id)
        {
            Event mEvent = db.Event.Find(id);
            return View(mEvent);
        }

        //
        // GET: /Events/Create

        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Category, "CategoryId", "Name");
            return View();
        } 

        //
        // POST: /Events/Create

        [HttpPost]
        public ActionResult Create(Event mEvent)
        {
            if (ModelState.IsValid)
            {
                mEvent.DateCreated = DateTime.Now;
                mEvent.Username = User.Identity.Name;
                db.Event.Add(mEvent);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.CategoryId = new SelectList(db.Category, "CategoryId", "Name", mEvent.CategoryId);
            return View(mEvent);
        }
        
        //
        // GET: /Events/Edit/5
 
        public ActionResult Edit(int id)
        {
            Event mEvent = db.Event.Find(id);
            ViewBag.CategoryId = new SelectList(db.Category, "CategoryId", "Name", mEvent.CategoryId);
            return View(mEvent);
        }

        //
        // POST: /Events/Edit/5

        [HttpPost]
        public ActionResult Edit(Event mEvent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mEvent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Category, "CategoryId", "Name", mEvent.CategoryId);
            return View(mEvent);
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

    }
}