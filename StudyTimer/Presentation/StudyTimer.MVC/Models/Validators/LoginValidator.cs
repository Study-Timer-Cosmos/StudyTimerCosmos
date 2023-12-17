using FluentValidation;
using StudyTimer.MVC.Models.Auth;

namespace StudyTimer.MVC.Models.Validators
{
    public class LoginValidator: AbstractValidator<AuthLoginViewModel>
    {
        public LoginValidator()
        {
            RuleFor(x=>x.Email).NotEmpty()
                .WithMessage("Please fill in the email field.")
                .NotNull()
                .WithMessage("Please fill in the email field.")
                .MaximumLength(20).WithMessage("Email cannot be longer than 20 characters")
                .MinimumLength(8).WithMessage("Email must be at least 8 characters long");

            RuleFor(x => x.Password).NotEmpty()
                .WithMessage("Please fill in the password field.")
                .NotNull()
                .WithMessage("Please fill in the password field.")
                .MinimumLength(6).WithMessage("Password must at least 6 characters long")
                .MaximumLength(20).WithMessage("Password cannot be longer than 20 characters");
            
        }

    }
}
