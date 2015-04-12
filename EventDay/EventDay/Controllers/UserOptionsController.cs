using EventDay.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace EventDay.Controllers
{
    public class UserOptionsController : Controller
    {
        private EventContext db = new EventContext();

        [Authorize]
        public ActionResult Index()
        {         
            //var events = db.Event.Where(b => b.Username == User.Identity.Name).OrderBy(x => x.DateBegin).OrderBy(x => x.HourBegin);

            //select * from Event where EventId in (select Event from JoinEvent where UserName=Current) or UserName=Current; 
            var pom = db.JoinEvent.Where(u => u.Username == User.Identity.Name).Select(u => u.EventId);
            var events = db.Event.Where(b => pom.Contains(b.EventId) || b.Username == User.Identity.Name);

            return View(events);           
        }

    }
}
