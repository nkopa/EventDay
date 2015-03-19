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

        //
        // GET: /Events/Details/5
        /*
       public ViewResult Details(int id)
        {
            Event mEvent = db.Event.Find(id);
            return View(mEvent);
        }
        */
        //
        // GET: /Events/Create
        /*
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Category, "CategoryId", "Name");
            return View();
        } 
        */
        //
        // POST: /Events/Create
        /*
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
        */
        //
        // GET: /Events/Edit/5
 /*
        public ActionResult Edit(int id)
        {
            Event mEvent = db.Event.Find(id);
            ViewBag.CategoryId = new SelectList(db.Category, "CategoryId", "Name", mEvent.CategoryId);
            return View(mEvent);
        }
*/
        //
        // POST: /Events/Edit/5
/*
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
*/
        //
        // GET: /Events/Delete/5
 /*
        public ActionResult Delete(int id)
        {
            Event mEvent = db.Event.Find(id);
            return View(mEvent);
        }
*/
        //
        // POST: /Events/Delete/5

        /*
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
           Event mEvent = db.Event.Find(id);
           db.Event.Remove(mEvent);
            db.SaveChanges();
            return RedirectToAction("Index");
       }
        */
        /*
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
        */
        /*
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
         */
    }
}