using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Accoon.BuildingBlocks.WebHosting
{
    public static class WebHostExtensions
    {
        public static IWebHost MigrateDbContext<TContext>(this IWebHost webHost, Action<TContext, IServiceProvider> seeder) where TContext : Microsoft.EntityFrameworkCore.DbContext
        {
            // get service
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                // get context
                var context = services.GetService<TContext>();

                try
                {
                    // call migrations and seeding methods
                    InvokeSeeder(seeder, context, services);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return webHost;
        }

        private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext context, IServiceProvider services)
         where TContext : Microsoft.EntityFrameworkCore.DbContext
        {
            // run context migrations
            //context.Database.Migrate();

            // ?????????????
            seeder(context, services);
        }


    }
}
