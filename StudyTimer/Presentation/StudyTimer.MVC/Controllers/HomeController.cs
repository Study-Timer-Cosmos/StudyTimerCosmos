using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudyTimer.MVC.Models;
using StudyTimer.MVC.Models.Home;
using StudyTimer.MVC.Services;
using System.Diagnostics;

namespace StudyTimer.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly StudySessionManager _studySessionManager;
        private readonly IToastService _toastService;

        public HomeController(StudySessionManager studySessionManager, IToastService toastService)
        {
            _studySessionManager = studySessionManager;
            _toastService = toastService;
        }

        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
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

            return RedirectToAction("Login", "Auth");
        }

        [HttpPost]
        public IActionResult CreateAsync(HomeCreateStudySessionViewModel viewModel)
        {
            if (User.Identity.IsAuthenticated)
            {
                HomeCreateStudySessionResponseModel responseModel = _studySessionManager.Create(viewModel, User);

                if (responseModel.Succeeded)
                {
                    _toastService.SuccessMessage("Success Create Study Sesssion");
                    return RedirectToAction(nameof(Index));
                }
            }

            _toastService.FailureMessage("Error");

            return RedirectToAction(nameof(Index));
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
