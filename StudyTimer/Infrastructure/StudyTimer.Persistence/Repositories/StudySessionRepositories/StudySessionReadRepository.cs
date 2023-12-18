using Microsoft.EntityFrameworkCore;
using StudyTimer.Application.Repositories.StudySessionRepositories;
using StudyTimer.Domain.Entities;
using StudyTimer.Persistence.Contexts;

namespace StudyTimer.Persistence.Repositories.StudySessionRepositories
{
    public class StudySessionReadRepository : ReadRepository<StudySession, Guid>, IStudySessionReadRepository
    {
        public StudySessionReadRepository(StudyTimerDbContext context) : base(context) { }
    }
}
