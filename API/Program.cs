using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using Microsoft.EntityFrameworkCore;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            //https://docs.microsoft.com/en-us/aspnet/core/fundamentals/?view=aspnetcore-6.0&tabs=windows
            //https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-6.0
            var host = CreateHostBuilder(args).Build();  // We are building our Host in variable called 'host', to make clean-up easier.

            using var scope = host.Services.CreateScope();  // Once we're with application, scope will be disposed of along with host (easier clean-up)

            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<DataContext>(); // Service Locator pattern https://stackify.com/service-locator-pattern/
                await context.Database.MigrateAsync();
                await Seed.SeedData(context); // We're typing await, because we are going to wait for Async Method seed.
            } 
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>(); // We are instantiating an object from the ILogger interface, for the Program class.
                logger.LogError(ex, "An error occurred during migration");
            }

            await host.RunAsync();  // This is what actually starts the application. It doesn't hurt to make all methods async.
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
