using StudyTimer.Application.Repositories.StudySessionRepositories;
using StudyTimer.Domain.Entities;
using StudyTimer.Persistence.Contexts;

namespace StudyTimer.Persistence.Repositories.StudySessionRepositories
{
    public class StudySessionWriteRepository : WriteRepository<StudySession, Guid>, IStudySessionWriteRepository
    {
        public StudySessionWriteRepository(StudyTimerDbContext context) : base(context)
        {
        }
    }
}
