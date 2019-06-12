using Accoon.BuildingBlocks.EventBus;
using Accoon.BuildingBlocks.EventBus.Abstractions;
using Accoon.BuildingBlocks.EventBusRabbitMQ;
using Accoon.CQRSCAApi.Application.Infastructure.AutoMapper;
using Accoon.CQRSCAApi.Application.Interfaces;
using Accoon.CQRSCAApi.Application.UserCases.CreateCustomer;
using Accoon.CQRSCLApi.Infastructure;
using Accoon.CQRSCLApi.Persistence;
using Autofac;
using AutoMapper;
using Autofac.Extensions.DependencyInjection;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Data.Common;
using System.Reflection;


namespace Accoon.CQRSCAApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // register auto mapper
            services.AddAutoMapper(new Assembly[] { typeof(AutoMapperProfile).GetTypeInfo().Assembly });

            // register db context and migration assebly
            var connectionString = Configuration.GetConnectionString("CQRSCADbContext").ToString();
            services.AddDbContext<CqrscaDbContext>
                (options => options.UseSqlServer(connectionString, x => x.MigrationsAssembly("Accoon.CQRSCLApi.Domain")));

            services.AddTransient<ICqrscaDbContext, CqrscaDbContext>();

            // add mvc             
            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_2);

            // swagger 
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

            services.AddMediatR(typeof(CreateCustomerHandler).GetTypeInfo().Assembly);
            services.AddTransient<INotificationService, NotificationService>();

            services.AddEventBus(Configuration).AddIntegrationServices(Configuration);

            var container = new ContainerBuilder();
            container.Populate(services);
            return new AutofacServiceProvider(container.Build());


        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });


            app.UseMvc();
        }

       
    }

    public static class CustomExtensionMethods
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            var subscriptionClientName = "Customer";


            services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
            {
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                var retryCount = 5;
                //if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
                //{
                //    retryCount = int.Parse(configuration["EventBusRetryCount"]);
                //}

                return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, iLifetimeScope, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
            });


            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            return services;
        }

        public static IServiceCollection AddIntegrationServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddTransient<Func<DbConnection, IIntegrationEventLogService>>(
            //    sp => (DbConnection c) => new IntegrationEventLogService(c));

            //services.AddTransient<ICatalogIntegrationEventService, CatalogIntegrationEventService>();

           
            
                services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
                {
                    //var settings = sp.GetRequiredService<IOptions<CatalogSettings>>().Value;
                    var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

                    var factory = new ConnectionFactory()
                    {
                        HostName = "localhost",// configuration["EventBusConnection"],
                        UserName="guest",
                        Password = "guest",
                    };

                    //if (!string.IsNullOrEmpty(configuration["EventBusUserName"]))
                    //{
                    //    factory.UserName = configuration["EventBusUserName"];
                    //}

                    //if (!string.IsNullOrEmpty(configuration["EventBusPassword"]))
                    //{
                    //    factory.Password = configuration["EventBusPassword"];
                    //}

                    var retryCount = 5;
                    //if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
                    //{
                    //    retryCount = int.Parse(configuration["EventBusRetryCount"]);
                    //}

                    return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
                });
            

            return services;
        }


    }


}
