using System;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;

namespace Portfolio.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Project()
        {
            Project project = new Project();
            var topThree = project.GetTopThreeProjects();
            return View(topThree);
        }
    }
}
