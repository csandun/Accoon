using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Accoon.BuildingBlocks.Common.Infastructure
{
    public static class WebHostExtensions
    {
        public static IWebHost MigrateDbContext<TContext>(this IWebHost webHost, Action<TContext, IServiceProvider> seeder) where TContext : DbContext
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
            context.Database.Migrate();

            // ?????????????
            seeder(context, services);
        }




    }
}
