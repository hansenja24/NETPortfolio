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
            return View(db.Reviews.Include(items => items.Products).ToList());
        }

        public IActionResult Details(int id)
        {
            var thisReview = db.Reviews.FirstOrDefault(items => items.ReviewId == id);
            return View(thisReview);
        }

        public IActionResult Create()
        {
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Review item)
        {
            db.Reviews.Add(item);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var thisReview = db.Reviews.FirstOrDefault(items => items.ReviewId == id);
            ViewBag.ProductId = new SelectList(db.Products, "ProductId", "Name");
            return View(thisReview);
        }

        [HttpPost]
        public IActionResult Edit(Review item)
        {
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var thisReview = db.Reviews.FirstOrDefault(items => items.ReviewId == id);
            return View(thisReview);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var thisReview = db.Reviews.FirstOrDefault(items => items.ReviewId == id);
            db.Reviews.Remove(thisReview);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}