

namespace ExcellenceQuest.Business
{
    using ExcellenceQuest.Business.Contracts;
    using ExcellenceQuest.Business.Shared;
    using ExcellenceQuest.Repository.Contracts;
    using ExcellenceQuest.Repository.Implementation;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class Startup
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            
           
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEmployeeRespository,EmployeeRepository>();
            services.AddScoped<IEmployeeAchievementRepository, EmployeeAchievementRepository>();
            services.AddScoped<IKeyPerformanceIndexRepository, KeyPerformanceIndexRepository>();
            services.AddScoped<IKPISuccessCriteriaRepository, KPISuccessCriteriaRepository>();
            services.AddScoped<IBadgeConfigurationRepository, BadgeConfigurationRepository>();
            services.AddScoped<IBadgeRepository, BadgeRepository>();
        }
    }
}
