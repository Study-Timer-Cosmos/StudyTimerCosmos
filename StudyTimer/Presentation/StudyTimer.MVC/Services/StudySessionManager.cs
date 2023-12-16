using Microsoft.AspNetCore.Identity;
using StudyTimer.Domain.Entities;
using StudyTimer.Domain.Identity;
using StudyTimer.MVC.Models.Home;
using StudyTimer.Persistence.Contexts;
using System.Security.Claims;

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

        public HomeCreateStudySessionResponseModel Create(HomeCreateStudySessionViewModel model, ClaimsPrincipal user)
        {

            DateTimeOffset now = DateTimeOffset.UtcNow;

            Guid id = Guid.NewGuid();
            Guid dutyId = Guid.NewGuid();
            StudySession studySession = new()
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
            };
            _context.StudySessions.Add(studySession);
            var userId = _userManager.GetUserId(user);
            User currentUser = _context.Users.FirstOrDefault(x => x.Id == Guid.Parse(userId));
            if(currentUser.Sessions is null)
            {
            currentUser.Sessions = new List<UserStudySession>()
                {
                 new()
                {
                    UserId=Guid.Parse(userId),
                    StudySessionId= studySession.Id,
                    CreatedByUserId= userId,
                    CreatedOn = DateTime.UtcNow,


                }

            };

            }
            else
            {
                currentUser.Sessions.Add(new()
                {
                    UserId = Guid.Parse(userId),
                    StudySessionId = studySession.Id,
                    CreatedByUserId = userId,
                    CreatedOn = DateTime.UtcNow,


                });
            }
            



            _context.SaveChanges();

            return new()
            {
                Succeeded = true
            };
        }

        public HomeGetStatisticsViewModel Statistics(ClaimsPrincipal user)
        {
            var userId = _userManager.GetUserId(user);
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            User User = _context.Users.FirstOrDefault(x => x.Id == Guid.Parse(userId));

            List<UserStudySession> sessions = _context.UserStudySessions
                .Where(x => x.UserId == Guid.Parse(userId))
                .ToList();

            int totalSessions = sessions.Count();

            TimeSpan totalTime = TimeSpan.Zero;

            foreach (var session in sessions)
            {
                StudySession studySession = _context.StudySessions
                    .FirstOrDefault(x => x.Id == session.StudySessionId);
                TimeSpan time = (studySession.EndTime - studySession.StartTime);
                totalTime += time;
            }

            List<Duty> allDuties = new List<Duty>();
            int completedDutiesCount =0;
            foreach (var userSession in sessions)
            {
                StudySession studySession = userSession.StudySession;
                List<Duty> sessionDuties = _context.Duties.Where(x => x.SessionId == studySession.Id).ToList();
                allDuties.AddRange(sessionDuties);
            }
            completedDutiesCount = allDuties.Where(x=>x.isFinished).Count();
            int incompletedDutiesCount = allDuties.Count() - completedDutiesCount;


            string mostRepeatedTopic = allDuties
                .GroupBy(x => x.Topic.ToLower())
                .OrderByDescending(x => x.Count())
                .Select(x => x.Key)
                .FirstOrDefault();


            HomeGetStatisticsViewModel viewModel = new()
            {
                TotalStudySessions = totalSessions,
                TotalStudyTime = totalTime,
                MostStudiedTopic = mostRepeatedTopic,
                TotalCompletedDuties = completedDutiesCount,
                TotalIncompleteDuties = incompletedDutiesCount,
                LastStudySessionDate = DateTime.Now,



                //public string MostStudiedCategory { get; set; }
                //public int TotalCompletedDuties { get; set; }
                //public int TotalIncompleteDuties { get; set; }
                //public DateTimeOffset LastStudySessionDate { get; set; }

            };
            return viewModel;
        }

        public List<Category> GetUserCategories(ClaimsPrincipal user)
        {
            List<UserStudySession> userStudySessions = _context.UserStudySessions.Where(x => x.UserId == Guid.Parse(_userManager.GetUserId(user))).ToList();
            List<Duty> duties = new();
            foreach (UserStudySession userStudySession in userStudySessions)
            {
                duties.AddRange(_context.Duties.Where(x => x.SessionId == userStudySession.StudySessionId).ToList());
            }
            List<Category> categories = new();
            foreach (Duty duty in duties)
            {
                categories.AddRange(_context.Categories.Where(x => x.DutyId == duty.Id).ToList());
            }
            return categories;
        }
    }
}
