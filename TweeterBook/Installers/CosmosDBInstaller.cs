using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using TweeterBook.Services;
using Microsoft.AspNetCore.Identity;
using TweeterBook.Data;

namespace TweeterBook.Installers
{
    public class CosmosDBInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CosmosDataContext>(options =>
                                        options.UseCosmos(configuration.GetConnectionString("CosmosEndPoint"),
                                        configuration.GetConnectionString("CosmosKey"),
                                        configuration.GetConnectionString("CosmosDatabase"))
                                    );

            services.AddScoped<ICosmosPostServices, CosmosPostServices>();
        }
    }
}
