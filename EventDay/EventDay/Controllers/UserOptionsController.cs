using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EventDay.Controllers
{
    public class UserOptionsController : Controller
    {

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

    }
}
