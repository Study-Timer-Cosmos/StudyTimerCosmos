using Microsoft.AspNetCore.Identity;
using StudyTimer.Domain.Entities;
using StudyTimer.Domain.Identity;
using StudyTimer.MVC.Models.Home;
using StudyTimer.Persistence.Contexts;

namespace StudyTimer.MVC.Services
{
    public class StudySessionManager
    {
        private readonly StudyTimerDbContext _context;
        private readonly UserManager<User> _userManager;

        public StudySessionManager(StudyTimerDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public HomeCreateStudySessionResponseModel Create(HomeCreateStudySessionViewModel model, System.Security.Claims.ClaimsPrincipal user)
        {
            DateTimeOffset now = DateTimeOffset.UtcNow;

            Guid id = Guid.NewGuid();
            Guid dutyId = Guid.NewGuid();
            _context.StudySessions.Add(new()
            {
                Id = id,
                StartTime = now,
                EndTime = now.AddMinutes(model.SelectedTime),
                Duties = new List<Duty>()
                {
                    new()
                    {
                        Id = dutyId,
                        Topic = model.Topic,
                        isFinished = false,
                        TaskTime = TimeSpan.FromMinutes(model.SelectedTime),
                        SessionId = id,
                        Categories = new List<Category>()
                        {
                            new()
                            {
                                Id = Guid.NewGuid(),
                                Name = model.CategoryName,
                                Description = model.CategoryDescription,
                                DutyId = dutyId,
                            }
                        }
                    }
                },
                CreatedByUserId = _userManager.GetUserId(user),
                CreatedOn = DateTime.UtcNow
            });
            _context.SaveChanges();

            return new()
            {
                Succeeded = true
            };
        }
    }
}
