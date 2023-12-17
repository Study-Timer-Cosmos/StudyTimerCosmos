using FluentValidation;
using StudyTimer.MVC.Models.Auth;

namespace StudyTimer.MVC.Models.Validators
{
    public class RegisterValidator: AbstractValidator<AuthViewModel>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Email).NotEmpty()
                .WithMessage("Please fill in the email field.")
                .NotNull()
                .WithMessage("Please fill in the email field.")
                .MaximumLength(20).WithMessage("Email cannot be longer than 20 characters")
                .MinimumLength(8).WithMessage("Email must be at least 8 characters long");

            RuleFor(x => x.UserName)
                .NotEmpty()
                .NotNull()
                .WithMessage("Please fill in the UserName field")
                .MinimumLength(4).WithMessage("UserName must be at least 4 characters long.")
                .MaximumLength(15).WithMessage("Username cannot be longer than 15 characters");

            RuleFor(x => x.Password).NotEmpty()
                .WithMessage("Please fill in the password field.")
                .NotNull()
                .WithMessage("Please fill in the password field.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
                .MaximumLength(20).WithMessage("Password cannot be longer than 20 characters");
            

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .NotNull()
                .WithMessage("FirstName cannot be null")
                .MinimumLength(4).WithMessage("FirstName must be at least 4 characters long.")
                .MaximumLength(15).WithMessage("FirstName cannot be longer than 15 characters");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .NotNull()
                .WithMessage("LastName cannot be null")
                .MinimumLength(4).WithMessage("LastName must be at least 4 characters long.")
                .MaximumLength(15).WithMessage("LastName cannot be longer than 15 characters");

            

            RuleFor(x => x.Gender).NotNull().NotEmpty().WithMessage("Please select gender");


        }
    }
}
