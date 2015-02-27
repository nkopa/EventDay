using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventDay.Models;

namespace EventDay.Controllers
{ 
    public class CommentsController : Controller
    {
        private EventContext db = new EventContext();

  

        //
        // GET: /Comments/Create
        [Authorize]
        public ActionResult AddComment(int id)
        {
            ViewBag.CommentCategoryId = new SelectList(db.CommentCategory, "CommentCategoryId", "Name");
            var comment = new Comment();
            comment.EventId = id;

            return View(comment);

        }

        //
        // POST: /Comments/Create
        // [Authorize]
        [HttpPost]
        public ActionResult AddComment(Comment comment)
        {
            if (ModelState.IsValid)
            {
                comment.Username = User.Identity.Name;
                comment.DateCreated = DateTime.Now;
                db.Comment.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Details", "Events", new { id = comment.EventId });
            }

            return View(comment);
        }

        public ActionResult ListOfComments(int id) //lista komentarzy o danej recenzjoksiazce
        {


            var comments = db.Comment.Where(c => c.EventId.Equals(id));

            return View(comments.ToList());

        }

        public ActionResult SumNegative(int id)
        {

            int sum = db.Comment.Where(c => c.CommentCategoryId == 3 && c.EventId == id).Count();
            ViewData["Negative"] = sum;

            return PartialView("SumNegative");

        }

        public ActionResult SumPositive(int id)
        {

            int sum = db.Comment.Where(c => c.CommentCategoryId == 2 && c.EventId == id).Count();
            ViewData["Positive"] = sum;

            return PartialView("SumPositive");

        }

    }
}