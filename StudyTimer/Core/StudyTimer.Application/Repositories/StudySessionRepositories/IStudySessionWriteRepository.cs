using StudyTimer.Domain.Entities;

namespace StudyTimer.Application.Repositories.StudySessionRepositories
{
    public interface IStudySessionWriteRepository : IWriteRepository<StudySession, Guid>
    {
    }
}
