using StudyTimer.Application.Repositories.DutyRepositories;
using StudyTimer.Domain.Entities;
using StudyTimer.Persistence.Contexts;

namespace StudyTimer.Persistence.Repositories.DutyRepositories
{
    public class DutyReadRepository : ReadRepository<Duty, Guid>, IDutyReadRepository
    {
        public DutyReadRepository(StudyTimerDbContext context) : base(context)
        {
        }

        public List<Duty> GetFromSessionId(Guid id)
        {
            return Table.Where(x => x.SessionId == id).ToList();
        }
    }
}
