using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventDay.Models;

namespace EventDay.Controllers
{
    [Authorize(Roles="admin")]
    public class CategoryController : Controller
    {
        private EventContext db = new EventContext();

        //
        // GET: /Categoty/
        public ActionResult Index()
        {
            var categories = db.Category.OrderBy(c => c.Name).ToList();
            return View(categories);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Category.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        public ActionResult Edit(int id)
        {
            Category category = db.Category.Find(id);
            if (category == null) return HttpNotFound();

            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        public ActionResult Delete(int id)
        {
            Category category = db.Category.Include("Events").Where(c => c.CategoryId == id).First();
            if (category == null) return HttpNotFound();

            if (category.Events.Count == 0) ViewBag.IsError = 0;
            else ViewBag.IsError = 1;

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = db.Category.Find(id);
            db.Category.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
