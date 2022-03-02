using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace TweeterBook.Installers
{
    public static class InstallerExtensions
    {
        public static void InstallServicesInAssembly(this IServiceCollection services, IConfiguration configuration)
        {
            //Serch for all classes implementing the IInstaller interface, create an instance of them and cast them into IInstaller
            //This will essentially return MvcInstaller and DBInstaller
            var installers = typeof(Startup).Assembly.ExportedTypes.Where(x =>
                typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance).Cast<IInstaller>().ToList();

            //Configure Services
            installers.ForEach(installer => installer.InstallServices(services, configuration));
        }
    }
}
