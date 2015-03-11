using EventDay.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventDay.Controllers
{
    public class UserController : Controller
    {
        private EventContext db = new EventContext();

        //
        // GET: /User/

        public ActionResult Index()
        {
            var users = db.UserProfile;//.Select(b => b).Where(e => e.StatusId == "Active");
            return View(users);
        }

        //
        // GET: /User/Details/5

        public ActionResult Details(int id)
        {
            var user = db.UserProfile.Where(e => e.UserId == id);
            return View(user);
        }
       
        //
        // GET: /User/Edit/5
 
        public ActionResult Edit(int id)
        {
            UserProfile userProfile = db.UserProfile.Find(id);
            return View(userProfile);
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        public ActionResult Edit(UserProfile userProfile)//(int id, FormCollection collection)
        {
            userProfile.UpdateTime = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.Entry(userProfile).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(userProfile);
        }

        //
        // GET: /User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /User/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
