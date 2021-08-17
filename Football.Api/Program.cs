using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Football.Api.Data;
using Football.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Football.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;
                var context = provider.GetRequiredService<FootballContext>();
                var userManager = provider.GetRequiredService<UserManager<User>>();
                var roleManager = provider.GetRequiredService<RoleManager<Role>>();
                var logger = provider.GetRequiredService<ILogger<Program>>();
                try
                {
                    DatabaseInitializer.Seed(context,userManager,roleManager).Wait();
                }
                catch (Exception e)
                {
                    logger.LogError(e.Message);
                }
            }
            
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
