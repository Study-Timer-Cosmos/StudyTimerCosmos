using StudyTimer.Application.Repositories.UserRepositories;
using StudyTimer.Domain.Entities;
using StudyTimer.Persistence.Contexts;

namespace StudyTimer.Persistence.Repositories.UserStudySessionRepositories
{
    public class UserStudySessionReadRepository : ReadRepository<UserStudySession, Guid>, IUserStudySessionReadRepository
    {
        public UserStudySessionReadRepository(StudyTimerDbContext context) : base(context)
        {
        }

        public List<UserStudySession> GetFromUserId(Guid userId)
        {
            return Table.Where(x => x.UserId == userId).ToList();
        }
    }
}
