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

        public async Task<AuthRegisterResponseModel> RegisterAsync(AuthViewModel authViewModel)
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



            return new AuthRegisterResponseModel
            {
                Succeeded = identityResult.Succeeded,
                Errors = identityResult.Errors.Select(e => new AuthErrorModel()
                {
                    Code = e.Code,
                    Message = e.Description
                }),
                userToken = HttpUtility.UrlEncode(await _userManager.GenerateEmailConfirmationTokenAsync(user))
            };
        }
    }
}
