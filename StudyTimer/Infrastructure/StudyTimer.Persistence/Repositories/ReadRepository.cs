using Microsoft.EntityFrameworkCore;
using StudyTimer.Application.Repositories;
using StudyTimer.Domain.Common;
using StudyTimer.Persistence.Contexts;

namespace StudyTimer.Persistence.Repositories
{
    public class ReadRepository<T, TId> : IReadRepository<T, TId> where T : EntityBase<TId>
    {
        private readonly StudyTimerDbContext _context;

        public ReadRepository(StudyTimerDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public List<T> GetAll()
        {
            return Table.ToList();
        }

        public T GetById(TId id)
        {
            return Table.FirstOrDefault(x => x.Id.ToString() == id.ToString());
        }
    }
}
