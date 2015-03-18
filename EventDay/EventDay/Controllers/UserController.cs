using EventDay.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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
            var user = db.UserProfile.Where(e => e.UserId == id).First();
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
            var userToDelete = db.UserProfile.Find(id);
            if (userToDelete == null) return HttpNotFound();
            return View(userToDelete);
        }

        //
        // POST: /User/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var userToDelete = db.UserProfile.Find(id);
            if (userToDelete == null) return HttpNotFound();

            if (String.Compare(userToDelete.UserName, User.Identity.Name, true) == 0) FormsAuthentication.SignOut();

            db.UserProfile.Remove(userToDelete);
            db.SaveChanges();

            return RedirectToAction("Delete", "Account", new { username = userToDelete.UserName });
        }


        public ActionResult AddContact(int id)
        {
            UserProfile loggedUser = db.UserProfile.Where(u => u.UserName == User.Identity.Name).First();
            if (loggedUser == null) return HttpNotFound();

            var foundContact = db.UserContact.Where(c => c.UserOwnerId == loggedUser.UserId).Where(c => c.UserMemberId == id).ToList();
            if (foundContact.Count == 1)
            {
                //ViewBag.Message = "Ten użytkownik znajduje się już na liście twoich kontaktów.";
                return RedirectToAction("Details", new { id = id });
            }

            UserProfile userToContact = db.UserProfile.Find(id);
            if (userToContact == null) return HttpNotFound();

            UserContact newContact = new UserContact();
            newContact.UserMemberId = userToContact.UserId;
            newContact.UserOwnerId = loggedUser.UserId;
            db.UserContact.Add(newContact);
            db.SaveChanges();

            return RedirectToAction("Details", new { id = id });
        }

    }
}
