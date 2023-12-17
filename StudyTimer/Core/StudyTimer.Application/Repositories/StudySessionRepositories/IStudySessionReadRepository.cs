using StudyTimer.Domain.Entities;

namespace StudyTimer.Application.Repositories.StudySessionRepositories
{
    public interface IStudySessionReadRepository : IReadRepository<StudySession, Guid>
    {
    }
}
