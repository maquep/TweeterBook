using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TweeterBook.Data;
using TweeterBook.HealthChecks;

namespace TweeterBook.Installers
{
    public class HealthCheckInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddDbContextCheck<DataContext>()
                .AddCheck<RedisHealthCheck>("Redis");
        }
    }
}
