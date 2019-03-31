using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Accoon.Api.BussinessServices.Concretes.Services;
using Accoon.Api.BussinessServices.Interfaces.Services;
using Accoon.Api.DataServices.Concrete.Repositories;
using Accoon.Api.DataServices.Entities;
using Accoon.Api.DataServices.Interfaces.Repositories;
using Accoon.BuildingBlocks.Common.Concretes;
using Accoon.BuildingBlocks.Common.Interfaces;
using AutoMapper;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;

namespace Accoon.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // register automapper
            services.AddAutoMapper();

            // register mvc
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // swagger 
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

            // register db context and migration assebly
            var connectionString = @"Server=(localdb)\mssqllocaldb;Database=AccoonDatabase;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<AccoonDbContext>
                (options => options.UseSqlServer(connectionString, x => x.MigrationsAssembly("Accoon.Api.DataServices.Entities")));

            // Register interfaces and classes 
            // https://andrewlock.net/using-scrutor-to-automatically-register-your-services-with-the-asp-net-core-di-container/
            // register base classes
            services.AddTransient<IService, ServiceBase>();
            services.AddTransient(typeof(IRepository<,,>), typeof(RepositoryBase<,,>));
            services.AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

            // register repositories
            services.Scan(scan => scan
            .FromAssembliesOf(typeof(AddressRepository))
            .AddClasses(classes => classes.InExactNamespaceOf<AddressRepository>())
            .AsImplementedInterfaces()
            .WithTransientLifetime());

            // services
            services.Scan(scan => scan
            .FromAssembliesOf(typeof(AddressService))
            .AddClasses(classes => classes.InNamespaceOf<AddressService>().Where(c => c.Name.EndsWith("Service")))
            .AsImplementedInterfaces()
            .WithTransientLifetime());


            // Health checkings 
            // https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/implement-resilient-applications/monitor-app-health
            services.AddHealthChecks()
                .AddSqlServer(connectionString); // sql server health check
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // healthcheck middleware
            app.UseHealthChecks("/hc",
                new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

            // add serilog
            // https://ondrejbalas.com/using-serilog-with-asp-net-core-2-0/
            loggerFactory.AddSerilog();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui(HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");

            });


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
