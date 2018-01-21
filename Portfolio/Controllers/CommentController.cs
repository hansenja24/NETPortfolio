using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Portfolio.Models;

namespace Portfolio.Controllers
{
    public class CommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public IActionResult Index()
        {
            return View(db.Comments.Include(items => items.Posts).ToList());
        }

        public IActionResult Details(int id)
        {
            var thisComment = db.Comments.Include(items => items.Posts).ToList().FirstOrDefault(items => items.CommentId == id);
            return View(thisComment);
        }

        public IActionResult Create(int id)
        {
            var thisComment = db.Comments.FirstOrDefault(items => items.CommentId == id);
            ViewBag.PostId = new SelectList(db.Posts, "PostId", "Title");
            return View(thisComment);
        }

        [HttpPost]
        public IActionResult Create(Comment item)
        {
            db.Comments.Add(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var thisComment = db.Comments.Include(items => items.Posts).FirstOrDefault(items => items.CommentId == id);
            ViewBag.PostId = new SelectList(db.Posts, "PostId", "Title");
            return View(thisComment);
        }

        [HttpPost]
        public IActionResult Edit(Comment item)
        {
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var thisComment = db.Comments.FirstOrDefault(items => items.CommentId == id);
            return View(thisComment);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisComment = db.Comments.FirstOrDefault(items => items.CommentId == id);
            db.Comments.Remove(thisComment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}