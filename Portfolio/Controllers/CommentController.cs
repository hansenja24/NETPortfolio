using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Portfolio.Models;
using Portfolio.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;


namespace Portfolio.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public CommentsController(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.Comments.Include(items => items.Posts).ToList());
        }

        public IActionResult Details(int id)
        {
            var thisComment = _db.Comments.Include(items => items.Posts).ToList().FirstOrDefault(items => items.CommentId == id);
            return View(thisComment);
        }

        public IActionResult Create(int id)
        {
            var thisComment = _db.Comments.FirstOrDefault(items => items.CommentId == id);
            ViewBag.PostId = new SelectList(_db.Posts, "PostId", "Title");
            return View(thisComment);
        }

        [HttpPost]
        public IActionResult Create(Comment item)
        {
            _db.Comments.Add(item);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var thisComment = _db.Comments.Include(items => items.Posts).FirstOrDefault(items => items.CommentId == id);
            ViewBag.PostId = new SelectList(_db.Posts, "PostId", "Title");
            return View(thisComment);
        }

        [HttpPost]
        public IActionResult Edit(Comment item)
        {
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var thisComment = _db.Comments.FirstOrDefault(items => items.CommentId == id);
            return View(thisComment);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisComment = _db.Comments.FirstOrDefault(items => items.CommentId == id);
            _db.Comments.Remove(thisComment);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}