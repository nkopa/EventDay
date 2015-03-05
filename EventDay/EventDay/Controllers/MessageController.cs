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

            if (box == "received")
            {
                messages = db.Message.Where(m => m.ToUser == User.Identity.Name).OrderBy(m => m.SendDate).ToList();
                ViewBag.TypeBox = "odebrane";
            }
            else if (box == "sent")
            {
                messages = db.Message.Where(m => m.FromUser == User.Identity.Name).OrderBy(m => m.SendDate).ToList();
                ViewBag.TypeBox = "wysłane";
            }
            else return HttpNotFound();

            return View(messages);
        }


        public ActionResult MessageDetails(int id)
        {
            Message message = db.Message.Find(id);

            if (message == null || (message.FromUser != User.Identity.Name && message.ToUser != User.Identity.Name)) return HttpNotFound();

            if (message.IsRead == false) message.IsRead = true;
            return View(message);
        }


        public ActionResult NewMessage()
        {
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
