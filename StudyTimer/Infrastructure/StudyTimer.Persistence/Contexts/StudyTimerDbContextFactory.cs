using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyTimer.Domain.Identity;

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

            var connectionString = configuration.GetSection("Server=91.151.83.102;Port=5432;Database=Team7_StudyTimer;User Id=enesfeyzierginteam;Password=EmwWHaw48Z7LRBJz8ABALpPUN;").Value;

            optionsBuilder.UseNpgsql(connectionString);

            return new StudyTimerDbContext(optionsBuilder.Options);
        }
    }
}
