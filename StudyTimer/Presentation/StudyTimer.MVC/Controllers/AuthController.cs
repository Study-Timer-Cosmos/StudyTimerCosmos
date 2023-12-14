using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudyTimer.Domain.Identity;
using StudyTimer.MVC.Models;
using StudyTimer.MVC.Services;

namespace StudyTimer.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IToastService _toastService;
        private readonly AuthManager _authManager;
        private readonly IEmailService _emailService;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager,
          AuthManager authManager, IEmailService emailService, IToastService toastService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authManager = authManager;
            _emailService = emailService;
            _toastService = toastService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(Index), "Home");
            }

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
                //_toastNotification.AddErrorToastMessage("Your email or password is incorrect.");

                return View(loginViewModel);
            }

            var loginResult = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, true, false);

            if (!loginResult.Succeeded)
            {
                //_toastNotification.AddErrorToastMessage("Your email or password is incorrect.");

                return View(loginViewModel);
            }

            //_toastNotification.AddSuccessToastMessage($"Welcome {user.UserName}!");

            await MailSend();

            return RedirectToAction("Index", controllerName: "Home");
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(AuthViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
                return View(registerViewModel);

            AuthResponseModel authRegisterResponseModel = await _authManager.RegisterAsync(registerViewModel);

            if (!authRegisterResponseModel.Succeeded)
            {
                foreach (var error in authRegisterResponseModel.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Message);
                }

                return View(registerViewModel);
            }

            //await _emailService.PrepareAndSendVerifyEmail(authRegisterResponseModel.userToken, registerViewModel.Email);

            Console.WriteLine($"Verify Link: https://localhost:7154/Auth/VerifyEmail?email={registerViewModel.Email}&token={authRegisterResponseModel.UserToken}");

            _toastService.SuccessMessage("You've successfully registered to the application.");

            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        public async Task<IActionResult> VerifyEmailAsync(string email, string token)
        {
            AuthResponseModel authResponseModel = await _authManager.VerifyEmailAsync(email, token);

            if (authResponseModel.Succeeded)
            {
                _toastService.SuccessMessage("You've successfully verified your email.");

                return View();
            }

            foreach (AuthErrorModel error in authResponseModel.Errors)
            {
                ModelState.AddModelError(error.Code, error.Message);
            }

            _toastService.FailureMessage("We unfortunately couldn't find your email.");

            return RedirectToAction(nameof(Login));
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        public async Task<IActionResult> MailSend()
        {
            /*var message = new EmailMessage();
            message.From = "onboarding@resend.dev";
            message.To.Add("seyyitahmet.kilic@gmail.com");
            message.Subject = "Hello!";
            message.HtmlBody = "<div><strong>Greetings<strong> 👋🏻 from .NET</div>";

            // await _resend.EmailSendAsync(message);
            return RedirectToAction("Register");*/
            return Ok();
        }
    }


}


