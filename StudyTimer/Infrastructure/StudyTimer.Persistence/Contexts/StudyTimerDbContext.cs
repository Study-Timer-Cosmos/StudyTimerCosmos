using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudyTimer.Domain.Entities;
using StudyTimer.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StudyTimer.Persistence.Contexts
{
    public class StudyTimerDbContext : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Duty> Duties { get; set; }
        public DbSet<StudySession> StudySessions { get; set; }


        public StudyTimerDbContext(DbContextOptions<StudyTimerDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

    }
}
