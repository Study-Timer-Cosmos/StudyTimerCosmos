using Microsoft.Extensions.DependencyInjection;
using StudyTimer.Application.Repositories.StudySessionRepositories;
using StudyTimer.Persistence.Repositories.StudySessionRepositories;

namespace StudyTimer.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddScoped<IStudySessionReadRepository, StudySessionReadRepository>();
            services.AddScoped<IStudySessionWriteRepository, StudySessionWriteRepository>();
        }
    }
}
