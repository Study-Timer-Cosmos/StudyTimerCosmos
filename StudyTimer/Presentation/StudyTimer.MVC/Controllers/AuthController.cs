using Microsoft.AspNetCore.Mvc;
using StudyTimer.MVC.Models.Auth;
using StudyTimer.MVC.Services;

namespace StudyTimer.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly IToastService _toastService;
        private readonly AuthManager _authManager;
        private readonly IEmailService _emailService;

        public AuthController(AuthManager authManager, IEmailService emailService, IToastService toastService)
        {
            _authManager = authManager;
            _emailService = emailService;
            _toastService = toastService;
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

            AuthResponseModel authRegisterResponseModel = await _authManager.LoginAsync(loginViewModel);

            if (!authRegisterResponseModel.Succeeded)
            {
                _toastService.FailureMessage("Your email or password is incorrect.");

                foreach (var error in authRegisterResponseModel.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Message);
                }

                //ViewData["Errors"] = authResponse.Errors.Select(error => error.Message).ToList();
                return View(loginViewModel);
            }
            _toastService.SuccessMessage("You've successfully login to the application.");

            return RedirectToAction("Index", controllerName: "Home");
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

        [HttpPost]
        public async Task<IActionResult> RegisterAsync(AuthViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    // Log or debug the error messages
                    var errorMessage = error.ErrorMessage;
                }

                return View(registerViewModel);
            }

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

            string port = "7154";
            Console.WriteLine($"Verify Link: https://localhost:{port}/Auth/VerifyEmail?email={registerViewModel.Email}&token={authRegisterResponseModel.UserToken}");

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
            await _authManager.Logout();
            return RedirectToAction("Login");
        }
    }
}


