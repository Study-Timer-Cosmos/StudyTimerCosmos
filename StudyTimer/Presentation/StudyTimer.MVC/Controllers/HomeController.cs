using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudyTimer.Domain.Identity;
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
        //public IActionResult Statistics(ClaimsPrincipal user)
        //{
        //    var userId = _userManager.GetUserId(user);
        //    //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    User User = _context.Users.FirstOrDefault(x => x.Id == Guid.Parse(userId));
        //    int totalSessions =User.Sessions.ToList().Count;

        //    List<UserStudySession> sessions = User.Sessions.ToList();
        //    TimeSpan totalTime = TimeSpan.Zero; 
        //    foreach(var session in sessions) 
        //    {
        //        TimeSpan time = (session.StudySession.EndTime - session.StudySession.StartTime);
        //        totalTime += time;
        //    }

        //    List<Duty> allDuties = new List<Duty>();
        //    foreach (var userSession in sessions) 
        //    {
        //        StudySession studySession = userSession.StudySession;
        //        List<Duty> sessionDuties = studySession.Duties.ToList();
        //        allDuties.AddRange(sessionDuties);
        //    }

        //    string mostRepeatedTopic = allDuties
        //        .GroupBy(x => x.Topic.ToLower()) 
        //        .OrderByDescending(x => x.Count())
        //        .Select(x => x.Key)
        //        .FirstOrDefault();


        //    HomeGetStatisticsViewModel viewModel = new()
        //    {
        //        TotalStudySessions = totalSessions,
        //        TotalStudyTime = totalTime,
        //        MostStudiedTopic = mostRepeatedTopic,
        //        MostStudiedCategory = "bilmem ne",
        //        TotalCompletedDuties=0,
        //        TotalIncompleteDuties=5,
        //        LastStudySessionDate= DateTime.Now,



        ////public string MostStudiedCategory { get; set; }
        ////public int TotalCompletedDuties { get; set; }
        ////public int TotalIncompleteDuties { get; set; }
        ////public DateTimeOffset LastStudySessionDate { get; set; }

        //    };
        //    return View(viewModel);

       // }
        

    }
}
