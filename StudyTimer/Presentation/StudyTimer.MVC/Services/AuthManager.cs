using Microsoft.AspNetCore.Identity;
using StudyTimer.Domain.Identity;
using StudyTimer.MVC.Models;
using System.Web;

namespace StudyTimer.MVC.Services
{
    public class AuthManager
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthManager(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<AuthResponseModel> RegisterAsync(AuthViewModel authViewModel)
        {
            Guid userId = Guid.NewGuid();
            User user = new()
            {
                Id = userId,
                Email = authViewModel.Email,
                FirstName = authViewModel.FirstName,
                LastName = authViewModel.LastName,
                Gender = authViewModel.Gender,
                BirthDate = authViewModel.BirthDate.Value.ToUniversalTime(),
                UserName = authViewModel.UserName,
                CreatedOn = DateTime.UtcNow,
                CreatedByUserId = userId.ToString(),
            };
            IdentityResult identityResult = await _userManager.CreateAsync(user, authViewModel.Password);



            return new AuthResponseModel
            {
                Succeeded = identityResult.Succeeded,
                Errors = identityResult.Errors.Select(e => new AuthErrorModel()
                {
                    Code = e.Code,
                    Message = e.Description
                }),
                UserToken = HttpUtility.UrlEncode(await _userManager.GenerateEmailConfirmationTokenAsync(user))
            };
        }

        public async Task<AuthResponseModel> VerifyEmailAsync(string email, string token)
        {
            User? user = await _userManager.FindByEmailAsync(email);

            if (user is not null)
            {
                IdentityResult identityResult = await _userManager.ConfirmEmailAsync(user, token);

                return new AuthResponseModel
                {
                    Succeeded = identityResult.Succeeded,
                    Errors = identityResult.Errors.Select(e => new AuthErrorModel
                    {
                        Code = e.Code,
                        Message = e.Description
                    })
                };
            }
            return new AuthResponseModel
            {
                Succeeded = false,
                Errors = new List<AuthErrorModel>()
                {
                    new AuthErrorModel()
                    {
                        Code = "User Not Found",
                        Message = "We unfortunately couldn't find your email."
                    }
                }
            };
        }

        public async Task<AuthResponseModel> LoginAsync(AuthLoginViewModel authLoginViewModel)
        {
            User? user = await _userManager.FindByEmailAsync(authLoginViewModel.Email);

            if (user is not null)
            {
                Microsoft.AspNetCore.Identity.SignInResult loginResult = await _signInManager.PasswordSignInAsync(user, authLoginViewModel.Password, true, false);
                //dikkat Microsoft.AspNetCore.Identity ekledim.

                if (loginResult.Succeeded)
                {
                    return new()
                    {
                        Succeeded = true,
                        Username = user.UserName
                    };
                }
            }

            return new AuthResponseModel
            {
                Succeeded = false,
                Errors = new List<AuthErrorModel>()
                    {
                        new AuthErrorModel()
                        {
                            Code = "Email Or Password Incorrect",
                            Message = "Your email or password is incorrect."
                        }
                    }
            };
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

    }
}
