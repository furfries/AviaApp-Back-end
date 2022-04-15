using System.Collections.Generic;
using System.Threading.Tasks;
using AviaApp.Services;
using Data.Entities;
using Data.Initializers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AviaApp
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var userManager = services.GetRequiredService<UserManager<AviaAppUser>>();
                    var rolesManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    var cabinClassService = services.GetRequiredService<CabinClassService>();

                    var cabinClasses = new List<CabinClass>
                    {
                        new()
                        {
                            Name = "Economy",
                            PricePerCent = 0,
                        },
                        new()
                        {
                            Name = "Premium Economy",
                            PricePerCent = 30,
                        },
                        new()
                        {
                            Name = "Business",
                            PricePerCent = 60,
                        },
                        new()
                        {
                            Name = "First",
                            PricePerCent = 100,
                        },
                    };
                    await cabinClassService.AddCabinClassesAsync(cabinClasses);

                    await DbInitializer.InitializeAsync(userManager, rolesManager);
                }
                catch
                {
                    // ignored
                }
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}