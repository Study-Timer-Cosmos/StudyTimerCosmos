using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudyTimer.Domain.Entities;
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
            return View();
        }

        public IActionResult AddDuty()
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
                    },
                    Categories = _studySessionManager.GetUserCategories(User).ConvertAll(new Converter<Category, SelectListItem>(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    }))  
              
                }
                );
            }

            return RedirectToAction("Login", "Auth");
        }

        [HttpPost]
        public IActionResult CreateAsync(HomeCreateStudySessionViewModel viewModel)
        {
            if (User.Identity.IsAuthenticated)
            {
                Console.WriteLine("Category name: "+viewModel.CategoryName);
                if (viewModel.CategoryName is null)
                {
                    string id = viewModel.SelectedCategoryId;
                    Category category = _studySessionManager.GetCategoryById(id);
                    viewModel.CategoryDescription = category.Description;
                    viewModel.CategoryName = category.Name;
                    viewModel.CategoryId = category.Id;
                }

                HomeCreateStudySessionResponseModel responseModel = _studySessionManager.Create(viewModel, User);

                if (responseModel.Succeeded)
                {
                    var model = viewModel;
                    _toastService.SuccessMessage("Success Create Study Sesssion");
                    return RedirectToAction("StudySession", model);
                }
            }

            _toastService.FailureMessage("Error");

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public IActionResult StudySession(HomeCreateStudySessionViewModel model)
        {
            return View(model);
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

        [HttpGet]
        public IActionResult Statistics()
        {
            if (User.Identity.IsAuthenticated)
            {
                HomeGetStatisticsViewModel model = _studySessionManager.Statistics(User);
                return View(model);
            }
            return View();

        }

    }
}
