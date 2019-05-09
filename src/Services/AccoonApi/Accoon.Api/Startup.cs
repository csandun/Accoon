using Accoon.Api.BussinessServices.Concretes.HttpClients;
using Accoon.Api.BussinessServices.Concretes.Services;
using Accoon.Api.BussinessServices.Interfaces.HttpClients;
using Accoon.Api.DataServices.Concrete.Repositories;
using Accoon.Api.DataServices.Entities;
using Accoon.Api.Infastructure;
using Accoon.Api.Middlewares;
using Accoon.BuildingBlocks.Common.Concretes;
using Accoon.BuildingBlocks.Common.Interfaces;
using AutoMapper;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Linq;
using System.Reflection;

namespace Accoon.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // register automapper
            services.AddAutoMapper();

            // register mvc
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = context =>
                    {
                        var problems = new CustomBadRequest(context);
                        return new BadRequestObjectResult(problems);
                    };

                    options.ClientErrorMapping[404] = new ClientErrorData() { Link = "", Title = "Not found resources" };
                });

            // swagger 
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

            // register db context and migration assebly
            var connectionString = Configuration["ConnectionString"].ToString();
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

            // IHttpClientFactory
            // https://www.stevejgordon.co.uk/introduction-to-httpclientfactory-aspnetcore
            services.AddHttpClient<IGitHubClient, GithubClient>(client =>
            {
                client.BaseAddress = new Uri("https://api.github.com/");
                client.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
                client.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactoryTesting");
            });

            // Mini Profiler
            // http://anthonygiretti.com/2018/12/16/common-features-in-asp-net-core-2-2-webapi-profiling/
            // https://miniprofiler.com/dotnet/AspDotNetCore
            services.AddMiniProfiler(options =>
                  options.RouteBasePath = "/profiler"
               ).AddEntityFramework();


            // Caching
            // http://anthonygiretti.com/2018/12/17/common-features-in-asp-net-core-2-2-webapi-caching/
            // https://docs.microsoft.com/en-us/aspnet/core/performance/caching/memory?view=aspnetcore-2.2
            // in mrmory cache
            services.AddMemoryCache();
            // caching response for middlewares            
            //services.AddResponseCaching();
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

            // add Miniprofiler
            app.UseMiniProfiler();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui(HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.IndexStream = () => GetType().GetTypeInfo().Assembly.GetManifestResourceStream("Accoon.Api.Accoon.SwaggerIndex.html");

            });


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // handle error handling globaly using middleware
            // https://www.strathweb.com/2018/07/centralized-exception-handling-and-request-validation-in-asp-net-core/
            app.ConfigureExceptionHandler(env);


            // caching response for middlewares
            //app.UseResponseCaching();

            app.UseMvc();
        }
    }
}


// other 
// seq dockerize = https://blog.datalust.co/docker-developer-preview-1/


// 5-4 todo
// user profile, ExceptionMiddlewareExtensions, CustomBadRequest move to commonlib
// log exception to middleware
//
//
//
