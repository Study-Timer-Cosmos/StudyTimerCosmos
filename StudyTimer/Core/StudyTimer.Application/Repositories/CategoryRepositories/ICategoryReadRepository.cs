using StudyTimer.Domain.Entities;

namespace StudyTimer.Application.Repositories.CategoryRepositories
{
    public interface ICategoryReadRepository : IReadRepository<Category, Guid>
    {
        List<Category> GetFromDutyId(Guid id);
        Category? GetFromName(string name);
    }
}
