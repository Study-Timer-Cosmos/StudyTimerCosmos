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
                .SetBasePath(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName+ "\\Presentation\\StudyTimer.MVC\\")
                .AddJsonFile("appsettings.json")
                .Build();
            //C: \Users\seyyi\Documents\GitHub\Team7-Project\StudyTimer\Presentation\StudyTimer.MVC\appsettings.json
            //C:\Users\seyyi\Documents\GitHub\Team7-Project\StudyTimer\Infrastructure\StudyTimer.Persistence\StudyTimer.Persistence.csproj
            var optionsBuilder = new DbContextOptionsBuilder<StudyTimerDbContext>();

            var connectionString = configuration.GetSection("Team7PostgreSQLDB").Value;

            optionsBuilder.UseNpgsql(connectionString);

            return new StudyTimerDbContext(optionsBuilder.Options);
        }
    }
}
