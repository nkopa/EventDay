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
        public ActionResult Index(int UserId)
        {
            
            //AuthorId w nowym formularzu jeszcze nie załączonym 
            var userEvents = db.Event.Where(b => b.AuthorId == UserId).OrderBy(x => x.DateBeginEvent).OrderBy(x => x.HourBeginEvent);
            
            return View(userEvents);
        }

    }
}
