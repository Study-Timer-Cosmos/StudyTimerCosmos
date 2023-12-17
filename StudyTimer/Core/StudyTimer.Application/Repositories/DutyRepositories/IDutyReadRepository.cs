using StudyTimer.Domain.Entities;

namespace StudyTimer.Application.Repositories.DutyRepositories
{
    public interface IDutyReadRepository : IReadRepository<Duty, Guid>
    {
        List<Duty> GetFromSessionId(Guid id);
    }
}
