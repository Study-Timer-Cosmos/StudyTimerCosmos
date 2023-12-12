using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyTimer.Persistence.Contexts
{
    public class StudyTimerDbContextFactory : IDesignTimeDbContextFactory<StudyTimerDbContext>
    {
        public StudyTimerDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<StudyTimerDbContext>();

            var connectionString = configuration.GetSection("Team7PostgreSQLDB").Value;

            optionsBuilder.UseNpgsql(connectionString);

            return new StudyTimerDbContext(optionsBuilder.Options);
        }
    }
}
