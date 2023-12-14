using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudyTimer.MVC.Models;
using System.Diagnostics;

namespace StudyTimer.MVC.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View(new HomeCreateStudySessionViewModel()
            {
                TimeOptions = new List<SelectListItem>
        {
            new SelectListItem { Value = "1", Text = "1 minute" },
            new SelectListItem { Value = "5", Text = "5 minutes" },
            new SelectListItem { Value = "10", Text = "10 minutes" },
            new SelectListItem { Value = "15", Text = "15 minutes" },
            new SelectListItem { Value = "30", Text = "30 minutes" }
        }
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
