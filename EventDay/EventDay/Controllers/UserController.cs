using EventDay.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
        [Authorize(Roles="admin")]
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
            ViewBag.VoiwodeshipList = CreateVoiwodeshipList();
            return View(userProfile);
        }

        //
        // POST: /User/Edit/5

        [HttpPost]
        public ActionResult Edit(UserProfile userProfile, HttpPostedFileBase fileProfileImage)//(int id, FormCollection collection)
        {
            userProfile.UpdateTime = DateTime.Now;

            string dateCreated = userProfile.UpdateTime.ToString().Replace(" ", "").Replace(":", "").Replace("-", "").Replace("/", "");
            if (fileProfileImage != null && fileProfileImage.ContentLength > 0)
            {
                string fileName = userProfile.UserId + dateCreated + "P" + Path.GetFileName(fileProfileImage.FileName);
                string path = Path.Combine(Server.MapPath("~/Content/Uploads"), fileName);
                fileProfileImage.SaveAs(path);

                userProfile.ProfileImage = fileName;
            }

                if (ModelState.IsValid)
                {
                    db.Entry(userProfile).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Details/" + userProfile.UserId);
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

        public ActionResult ShowUserEvents(int id)
        {
            var user = db.UserProfile.Find(id);
            if (user == null) return HttpNotFound();

            List<Event> userEvents;
            if (User.IsInRole("admin")) userEvents = db.Event.Where(e => String.Compare(e.Username, user.UserName, true) == 0).ToList();
            else userEvents = db.Event.Where(e => String.Compare(e.Username, user.UserName, true) == 0).Where(e => String.Compare(e.AccessId, "Widoczne", false) == 0).ToList();
            if (userEvents == null) return HttpNotFound();

            ViewBag.UserName = user.UserName;
            return View(userEvents);
        }

        public ActionResult EventDetails(int id)
        {
            return RedirectToAction("Details", "Events", new { id = id });
        }

        /**/
        public List<SelectListItem> CreateVoiwodeshipList()
        {
            List<SelectListItem> Voiwodeship = new List<SelectListItem>();

            Voiwodeship.Add(new SelectListItem { Text = "dolnośląskie", Value = "dolnośląskie" });
            Voiwodeship.Add(new SelectListItem { Text = "kujawsko-pomorskie", Value = "kujawsko-pomorskie" });
            Voiwodeship.Add(new SelectListItem { Text = "lubelskie", Value = "lubelskie" });
            Voiwodeship.Add(new SelectListItem { Text = "lubuskie", Value = "lubuskie" });
            Voiwodeship.Add(new SelectListItem { Text = "łódzkie", Value = "łódzkie" });
            Voiwodeship.Add(new SelectListItem { Text = "małopolskie", Value = "małopolskie" });
            Voiwodeship.Add(new SelectListItem { Text = "mazowieckie", Value = "mazowieckie" });
            Voiwodeship.Add(new SelectListItem { Text = "opolskie", Value = "opolskie" });
            Voiwodeship.Add(new SelectListItem { Text = "podkarpackie", Value = "podkarpackie" });
            Voiwodeship.Add(new SelectListItem { Text = "podlaskie", Value = "podlaskie" });
            Voiwodeship.Add(new SelectListItem { Text = "pomorskie", Value = "pomorskie" });
            Voiwodeship.Add(new SelectListItem { Text = "śląskie", Value = "śląskie" });
            Voiwodeship.Add(new SelectListItem { Text = "świętokrzyskie", Value = "świętokrzyskie" });
            Voiwodeship.Add(new SelectListItem { Text = "warmińsko-mazurskie", Value = "warmińsko-mazurskie" });
            Voiwodeship.Add(new SelectListItem { Text = "wielkopolskie", Value = "wielkopolskie" });
            Voiwodeship.Add(new SelectListItem { Text = "zachodniopomorskie", Value = "zachodniopomorskie" });

            //ViewBag.ViowodeshipList = Viowodeship;
            return Voiwodeship;
        }

    }
}
