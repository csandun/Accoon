using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Accoon.Api.DataServices.Entities;
using Accoon.Api.Infastructure;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Accoon.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // get configurations
            var configuration = GetConfiguration();

            // get builded host
            var host = BuildWebHost(configuration, args);

            // run migrations using extension method
            host.MigrateDbContext<AccoonDbContext>((context, services) =>
            {
                context.Customers.Add(new DataServices.Entities.CustomEntities.CustomerEntity()
                {
                    Name = "Sandun",
                    Age = 25
                });
            });

            host.Run();
        }

        // get config settings and values
        private static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            var config = builder.Build();
            return builder.Build();
        }

        // get web host and build
        private static IWebHost BuildWebHost(IConfiguration configuration, string[] args) =>
              WebHost.CreateDefaultBuilder(args)
                  .CaptureStartupErrors(false)
                  .UseStartup<Startup>()
                 .UseContentRoot(Directory.GetCurrentDirectory())
                  .UseConfiguration(configuration)
                .UseSerilog()
                 .Build();

    }
}

