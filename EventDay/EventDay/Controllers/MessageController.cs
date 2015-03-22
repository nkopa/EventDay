using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventDay.Models;

namespace EventDay.Controllers
{
    public class MessageController : Controller
    {
        private EventContext db = new EventContext();

        //
        // GET: /Message/
        public ActionResult Index(string box = "received")
        {
            IList<Message> messages = null;

            if (String.Compare(box,"sent",false) == 0)
            {
                messages = db.Message.Where(m => m.FromUser == User.Identity.Name).OrderBy(m => m.SendDate).ToList();
                ViewBag.TypeBox = "wysłane";
            }
            else 
            {
                messages = db.Message.Where(m => m.ToUser == User.Identity.Name).OrderBy(m => m.SendDate).ToList();
                ViewBag.TypeBox = "odebrane";
            }

            return View(messages);
        }


        public ActionResult MessageDetails(int id)
        {
            Message message = db.Message.Find(id);

            if (message == null || (message.FromUser != User.Identity.Name && message.ToUser != User.Identity.Name)) return HttpNotFound();

            if (message.IsRead == false)
            {
                message.IsRead = true;
                db.Entry(message).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View(message);
        }


        public ActionResult NewMessage()
        {
            UserProfile loggedUser = db.UserProfile.Where(u => u.UserName == User.Identity.Name).First();
            if (loggedUser == null) return HttpNotFound();

            List<UserContact> contacts = db.UserContact.Include("UserMember").Where(c => c.UserOwnerId == loggedUser.UserId).ToList();
            if (contacts == null) return HttpNotFound();

            List<string> userContacts = new List<string>();
            //prowizoryczne ale obecnie nie umiem tego obejsc w modelu; jesli macie pomysl - poprawcie
            foreach (UserContact contact in contacts)
            {
                UserProfile user = db.UserProfile.Find(contact.UserMemberId);
                userContacts.Add(user.UserName);
            }
            ViewBag.Contacts = userContacts;

            return View();
        }

        [HttpPost]
        public ActionResult NewMessage(Message message)
        {
            if (ModelState.IsValid)
            {
                message.IsRead = false;
                message.FromUser = User.Identity.Name;
                message.SendDate = DateTime.Now;
                db.Message.Add(message);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(message);
        }


        public ActionResult EditMessage(int id)
        {
            Message message = db.Message.Find(id);
            return View(message);
        }

        [HttpPost]
        public ActionResult EditMessage(Message message)
        {
            if (ModelState.IsValid)
            {
                db.Entry(message).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(message);
        }


        public ActionResult DeleteMessage(int id)
        {
            Message message = db.Message.Find(id);
            return View(message);
        }

        [HttpPost, ActionName("DeleteMessage")]
        public ActionResult DeleteConfirmed(int id)
        {
            Message message = db.Message.Find(id);
            db.Message.Remove(message);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
