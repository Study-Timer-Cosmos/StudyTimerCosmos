using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudyTimer.Application.Models;
using StudyTimer.Domain.Identity;
using StudyTimer.MVC.Models;

namespace StudyTimer.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;

        private readonly SignInManager<User> _signInManager;

        //private readonly IToastNotification _toastNotification;
        //private readonly IResend _resend;
        private readonly IWebHostEnvironment _environment;

        public UserController(UserManager<User> userManager, SignInManager<User> signInManager,
            IWebHostEnvironment environment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _environment = environment;
        }

        [HttpGet]
        public IActionResult Register()
        {
            //if (User.Identity.IsAuthenticated)
            //{
            //    return RedirectToAction(nameof(Index), "Home");
            //}

            var registerViewModel = new AuthViewModel();


            return View(registerViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(AuthViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
                return View(registerViewModel);

            var userId = Guid.NewGuid();

            var user = new User()
            {
                Id = userId,
                Email = registerViewModel.Email,
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.SurName,
                Gender = registerViewModel.Gender,
                BirthDate = registerViewModel.BirthDate.Value.ToUniversalTime(),
                UserName = registerViewModel.UserName,
                CreatedOn = DateTime.UtcNow,
                CreatedByUserId = userId.ToString()
            };

            var identityResult = await _userManager.CreateAsync(user, registerViewModel.Password);

            if (!identityResult.Succeeded)
            {
                foreach (var error in identityResult.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return View(registerViewModel);
            }
        }
    }
}
