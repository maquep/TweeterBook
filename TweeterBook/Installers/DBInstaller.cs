using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using TweeterBook.Data;
using TweeterBook.Services;

namespace TweeterBook.Installers
{
    public class DBInstaller : IInstaller
    {
        public async void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<DataContext>();

            services.AddScoped<IPostServices, PostServices>();

            //var dbName = "TweeterBookSqlLite.db";

            //if (File.Exists(dbName))
            //    File.Delete(dbName);

            //await using var dbContext = new DataContext();
            //await dbContext.Database.EnsureCreatedAsync();
        }
    }
}
