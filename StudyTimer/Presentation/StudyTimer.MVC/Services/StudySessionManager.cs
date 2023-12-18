using Microsoft.AspNetCore.Identity;
using StudyTimer.Application.Repositories.CategoryRepositories;
using StudyTimer.Application.Repositories.DutyRepositories;
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
        private readonly IDutyReadRepository _dutyReadRepository;
        private readonly ICategoryReadRepository _categoryReadRepository;

        public StudySessionManager(StudyTimerDbContext context, UserManager<User> userManager, IStudySessionReadRepository studySessionReadRepository, IStudySessionWriteRepository studySessionWriteRepository, IUserStudySessionReadRepository userStudySessionReadRepository, IDutyReadRepository dutyReadRepository, ICategoryReadRepository categoryReadRepository)
        {
            _context = context;
            _userManager = userManager;
            _studySessionReadRepository = studySessionReadRepository;
            _studySessionWriteRepository = studySessionWriteRepository;
            _userStudySessionReadRepository = userStudySessionReadRepository;
            _dutyReadRepository = dutyReadRepository;
            _categoryReadRepository = categoryReadRepository;
        }

        public HomeCreateStudySessionResponseModel Create(HomeCreateStudySessionViewModel model, ClaimsPrincipal user)
        {

            DateTimeOffset now = DateTimeOffset.UtcNow;
            Guid id = Guid.NewGuid();
            Guid dutyId = Guid.NewGuid();
            Category? category = _categoryReadRepository.GetFromName(model.CategoryName);
            if (category is not null)
            {
                model.CategoryId = category.Id;
            }
            StudySession studySession;
            string userId = _userManager.GetUserId(user);
            DateTime dateTimeNow = DateTime.UtcNow;
            if (model.CategoryId is null)
            {

                studySession = new()
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
                                CreatedOn = dateTimeNow

                            }
                        },
                        CreatedByUserId = userId,
                        CreatedOn = dateTimeNow
                    }


                },
                    CreatedByUserId = userId,
                    CreatedOn = dateTimeNow
                };
            }
            else
            {

                studySession = new()
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
                           category
                        },
                        CreatedByUserId = userId,
                        CreatedOn = dateTimeNow
                    }


                },
                    CreatedByUserId = userId,
                    CreatedOn = dateTimeNow
                };
            }
            _studySessionWriteRepository.Add(studySession);
            User currentUser = _context.Users.FirstOrDefault(x => x.Id == Guid.Parse(userId));
            if (currentUser.Sessions is null)
            {
                currentUser.Sessions = new List<UserStudySession>()
                {
                 new()
                {
                    UserId=Guid.Parse(userId),
                    StudySessionId= studySession.Id,
                    CreatedByUserId= userId,
                    CreatedOn = dateTimeNow,
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
                    CreatedOn = dateTimeNow,


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
                List<Duty> sessionDuties = _dutyReadRepository.GetFromSessionId(studySession.Id);
                allDuties.AddRange(sessionDuties);
            }
            completedDutiesCount = allDuties.Where(x => x.isFinished).Count();
            int incompletedDutiesCount = allDuties.Count - completedDutiesCount;


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
                duties.AddRange(_dutyReadRepository.GetFromSessionId(userStudySession.StudySessionId));
            }
            List<Category> categories = new();
            foreach (Duty duty in duties)
            {
                categories.AddRange(_categoryReadRepository.GetFromDutyId(duty.Id));
            }

            return categories;
        }

        public Category? GetCategoryById(string id)
        {
            return _categoryReadRepository.GetById(Guid.Parse(id));
        }

    }
}
