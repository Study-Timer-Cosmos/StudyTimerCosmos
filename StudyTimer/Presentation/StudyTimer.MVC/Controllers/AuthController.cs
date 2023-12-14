using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudyTimer.Application.Models;
using StudyTimer.Domain.Identity;
using StudyTimer.MVC.Models;
using NToastNotify;
using Resend;

namespace StudyTimer.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;

        private readonly SignInManager<User> _signInManager;

        private readonly IToastNotification _toastNotification;

        private readonly IWebHostEnvironment _environment;

        private readonly IResend _resend;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager,
            IWebHostEnvironment environment, IToastNotification toast,IResend resend)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _environment = environment;
            _toastNotification = toast;
            _resend = resend;
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

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            var loginViewModel = new AuthLoginViewModel();

            return View(loginViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> LoginAsync(AuthLoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
                return View(loginViewModel);

            var user = await _userManager.FindByEmailAsync(loginViewModel.Email);

            if (user is null)
            {
                _toastNotification.AddErrorToastMessage("Your email or password is incorrect.");

                return View(loginViewModel);
            }

            var loginResult = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, true, false);

            if (!loginResult.Succeeded)
            {
                _toastNotification.AddErrorToastMessage("Your email or password is incorrect.");

                return View(loginViewModel);
            }

            _toastNotification.AddSuccessToastMessage($"Welcome {user.UserName}!");

            await MailSend();

            return RedirectToAction("Index", controllerName: "Home");
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
                LastName = registerViewModel.LastName,
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

           return RedirectToAction("Index", controllerName: "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> MailSend()
        {
            var message = new EmailMessage();
            message.From = "onboarding@resend.dev";
            message.To.Add("seyyitahmet.kilic@gmail.com");
            message.Subject = "Hello!";
            message.HtmlBody = "<div><strong>Greetings<strong> 👋🏻 from .NET</div>";

            await _resend.EmailSendAsync(message);
            return RedirectToAction("Register");
        }
    }


}


