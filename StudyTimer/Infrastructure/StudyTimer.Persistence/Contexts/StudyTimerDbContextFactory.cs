using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace StudyTimer.Persistence.Contexts
{
    public class StudyTimerDbContextFactory : IDesignTimeDbContextFactory<StudyTimerDbContext>
    {
        public StudyTimerDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                 .SetBasePath($"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName}\\Presentation\\StudyTimer.MVC\\")
                 .AddJsonFile("appsettings.json")
                 .Build();

            var optionsBuilder = new DbContextOptionsBuilder<StudyTimerDbContext>();

            var connectionString = configuration.GetSection("Team7PostgreSQLDB").Value;


            optionsBuilder.UseNpgsql(connectionString);

            return new StudyTimerDbContext(optionsBuilder.Options);
        }


    }
}
