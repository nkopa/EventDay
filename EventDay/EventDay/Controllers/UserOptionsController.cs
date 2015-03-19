using EventDay.Models;
using System;
using System.Collections.Generic;
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


           var events = db.Event.Where(b => b.Username == User.Identity.Name).OrderBy(x => x.DateBegin).OrderBy(x => x.HourBegin);
            //ViewBag.userEventsCreate = db.Event.Where(b => b.Username == User.Identity.Name).OrderBy(x => x.DateBegin).OrderBy(x => x.HourBegin);

           return View(events);
        }

    }
}
