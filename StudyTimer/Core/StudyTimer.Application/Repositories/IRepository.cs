using Microsoft.EntityFrameworkCore;
using StudyTimer.Domain.Common;

namespace StudyTimer.Application.Repositories
{
    public interface IRepository<T, TId> where T : EntityBase<TId>
    {
        DbSet<T> Table { get; }
    }
}
