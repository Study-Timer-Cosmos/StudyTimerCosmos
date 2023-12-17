using Microsoft.EntityFrameworkCore;
using StudyTimer.Application.Repositories;
using StudyTimer.Domain.Common;
using StudyTimer.Persistence.Contexts;

namespace StudyTimer.Persistence.Repositories
{
    public class WriteRepository<T, TId> : IWriteRepository<T, TId> where T : EntityBase<TId>
    {
        private readonly StudyTimerDbContext _context;

        public WriteRepository(StudyTimerDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public void Add(T entity)
        {
            Table.Add(entity);
        }

        public void Delete(TId id)
        {
            Table.Remove(Table.FirstOrDefault(x => Guid.Parse(x.Id.ToString()) == Guid.Parse(id.ToString())));
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
