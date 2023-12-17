using Microsoft.Extensions.DependencyInjection;
using StudyTimer.Application.Repositories.StudySessionRepositories;
using StudyTimer.Application.Repositories.UserRepositories;
using StudyTimer.Persistence.Repositories.StudySessionRepositories;
using StudyTimer.Persistence.Repositories.UserStudySessionRepositories;

namespace StudyTimer.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddScoped<IStudySessionReadRepository, StudySessionReadRepository>();
            services.AddScoped<IStudySessionWriteRepository, StudySessionWriteRepository>();
            services.AddScoped<IUserStudySessionReadRepository, UserStudySessionReadRepository>();
        }
    }
}
