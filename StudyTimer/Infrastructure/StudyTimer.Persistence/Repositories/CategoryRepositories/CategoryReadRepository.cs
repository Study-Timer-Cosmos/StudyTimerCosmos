using StudyTimer.Application.Repositories.CategoryRepositories;
using StudyTimer.Domain.Entities;
using StudyTimer.Persistence.Contexts;

namespace StudyTimer.Persistence.Repositories.CategoryRepositories
{
    public class CategoryReadRepository : ReadRepository<Category, Guid>, ICategoryReadRepository
    {
        public CategoryReadRepository(StudyTimerDbContext context) : base(context)
        {
        }

        public List<Category> GetFromDutyId(Guid id)
        {
            return Table.Where(x => x.DutyId == id).ToList();
        }

        public Category? GetFromName(string name)
        {
            return Table.FirstOrDefault(x => x.Name == name);
        }
    }
}
