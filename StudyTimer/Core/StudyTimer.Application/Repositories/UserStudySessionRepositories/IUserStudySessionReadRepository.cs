using StudyTimer.Domain.Entities;

namespace StudyTimer.Application.Repositories.UserRepositories
{
    public interface IUserStudySessionReadRepository : IReadRepository<UserStudySession, Guid>
    {
        List<UserStudySession> GetFromUserId(Guid userId);
    }
}
