using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventDay.Models;

namespace EventDay.Controllers
{
    public class UserProfileController : Controller
    {
        private EventContext db = new EventContext();

        //
        // GET: /UserProfile/
        public ActionResult Index()
        {
            var loggedUser = db.UserProfile.Where(u => u.UserName == User.Identity.Name);

            if (loggedUser == null) return HttpNotFound();

            return View(loggedUser);
        }

        public ActionResult Delete(string name)
        {
            var userToDelete = db.UserProfile.Where(u => u.UserName == name).First();
            if (userToDelete == null) return HttpNotFound();
            return RedirectToAction("Delete", "User", new { id = userToDelete.UserId });
        }

        public ActionResult Details(string name)
        {
            var userToShow = db.UserProfile.Where(u => u.UserName == name).First();
            if (userToShow == null) return HttpNotFound();
            return RedirectToAction("Details", "User", new { id = userToShow.UserId });
        }

        public ActionResult Contacts()
        {
            UserProfile loggedUser = db.UserProfile.Where(u => u.UserName == User.Identity.Name).First();
            if (loggedUser == null) return HttpNotFound();

            var contacts = db.UserContact.Include("UserMember").Where(c => c.UserOwnerId == loggedUser.UserId).ToList();
            if (contacts == null) return HttpNotFound();

            return View(contacts);
        }

        public ActionResult DeleteContact(int id)
        {
            UserContact contact = db.UserContact.Find(id);
            if (contact == null) return HttpNotFound();

            db.UserContact.Remove(contact);
            db.SaveChanges();

            return RedirectToAction("Contacts");
        }

    }
}
