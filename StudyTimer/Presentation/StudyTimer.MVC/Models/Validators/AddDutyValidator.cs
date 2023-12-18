using FluentValidation;
using StudyTimer.MVC.Models.Home;

namespace StudyTimer.MVC.Models.Validators
{
    public class AddDutyValidator : AbstractValidator<HomeCreateStudySessionViewModel>
    {
        public AddDutyValidator()
        {
            RuleFor(x => x.Topic).NotEmpty()
                .WithMessage("Please fill in the topic field.")
                .NotNull()
                .WithMessage("Please fill in the topic field.");
        }
    }
}
