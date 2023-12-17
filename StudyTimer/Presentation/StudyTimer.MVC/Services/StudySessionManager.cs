using Microsoft.AspNetCore.Identity;
using StudyTimer.Application.Repositories.StudySessionRepositories;
using StudyTimer.Application.Repositories.UserRepositories;
using StudyTimer.Domain.Entities;
using StudyTimer.Domain.Identity;
using StudyTimer.MVC.Models.Home;
using StudyTimer.Persistence.Contexts;
using StudyTimer.Persistence.Repositories.StudySessionRepositories;
using System.Security.Claims;

namespace StudyTimer.MVC.Services
{
    public class StudySessionManager
    {
        private readonly StudyTimerDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IStudySessionReadRepository _studySessionReadRepository;
        private readonly IStudySessionWriteRepository _studySessionWriteRepository;
        private readonly IUserStudySessionReadRepository _userStudySessionReadRepository;

        public StudySessionManager(StudyTimerDbContext context, UserManager<User> userManager, IStudySessionReadRepository studySessionReadRepository, IStudySessionWriteRepository studySessionWriteRepository, IUserStudySessionReadRepository userStudySessionReadRepository)
        {
            _context = context;
            _userManager = userManager;
            _studySessionReadRepository = studySessionReadRepository;
            _studySessionWriteRepository = studySessionWriteRepository;
            _userStudySessionReadRepository = userStudySessionReadRepository;
        }

        public HomeCreateStudySessionResponseModel Create(HomeCreateStudySessionViewModel model, ClaimsPrincipal user)
        {

            DateTimeOffset now = DateTimeOffset.UtcNow;
            Guid id = Guid.NewGuid();
            Guid dutyId = Guid.NewGuid();
            var userId = _userManager.GetUserId(user);
            User currentUser = _context.Users.FirstOrDefault(x => x.Id == Guid.Parse(userId));

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
                                CreatedByUserId = userId,
                                CreatedOn = DateTime.UtcNow

                            }
                        },
                        CreatedByUserId = userId,
                        CreatedOn = DateTime.UtcNow
                    }


                },
                CreatedByUserId = userId,
                CreatedOn = DateTime.UtcNow
            };
            _studySessionWriteRepository.Add(studySession);
            if (currentUser.Sessions is null)
            {
                currentUser.Sessions = new List<UserStudySession>()
                {
                 new()
                {
                    UserId=Guid.Parse(userId),
                    StudySessionId= studySession.Id,
                    CreatedByUserId= userId,
                    CreatedOn = DateTime.UtcNow,
                    IsDeleted= false,
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




            _studySessionWriteRepository.SaveChanges();

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

            List<UserStudySession> sessions = _userStudySessionReadRepository.GetFromUserId(Guid.Parse(userId));

            int totalSessions = sessions.Count();

            TimeSpan totalTime = TimeSpan.Zero;

            foreach (var session in sessions)
            {
                StudySession studySession = _studySessionReadRepository.GetById(session.StudySessionId);
                TimeSpan time = (studySession.EndTime - studySession.StartTime);
                totalTime += time;
            }

            List<Duty> allDuties = new List<Duty>();
            int completedDutiesCount = 0;
            foreach (var userSession in sessions)
            {
                StudySession studySession = userSession.StudySession;
                List<Duty> sessionDuties = _context.Duties.Where(x => x.SessionId == studySession.Id).ToList();
                allDuties.AddRange(sessionDuties);
            }
            completedDutiesCount = allDuties.Where(x => x.isFinished).Count();
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

            };
            return viewModel;
        }

        public List<Category> GetUserCategories(ClaimsPrincipal user)
        {
            List<UserStudySession> userStudySessions = _userStudySessionReadRepository.GetFromUserId(Guid.Parse(_userManager.GetUserId(user)));
            List<Duty> duties = new();
            foreach (UserStudySession userStudySession in userStudySessions)
            {
                duties.AddRange(_context.Duties.Where(x => x.SessionId == userStudySession.StudySessionId).ToList());
            }
            List<Category> categories = new();
            foreach (Duty duty in duties)
            {
                categories.AddRange(_context.Categories.Where(x => x.DutyId == duty.Id).ToList()
);
            }

            return categories;
        }

        public Category? GetCategoryById(string id)
        {
            return _context.Categories.FirstOrDefault(x => x.Id == Guid.Parse(id));
        }

    }
}
