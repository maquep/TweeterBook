using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TweeterBook.Services;

namespace TweeterBook.Installers
{
    public class DBInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IPostServices, PostServices>();
        }
    }
}
