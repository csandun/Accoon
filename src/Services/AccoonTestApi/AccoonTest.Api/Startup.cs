using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Accoon.BuildingBlocks.EventBus;
using Accoon.BuildingBlocks.EventBus.Abstractions;
using Accoon.BuildingBlocks.EventBusRabbitMQ;
using AccoonTest.Api.IntergrationEvents.EventHandling;
using AccoonTest.Api.IntergrationEvents.Events;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace AccoonTest.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
            {
                //var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

                //var factory = new ConnectionFactory()
                //{
                //    HostName = Configuration["EventBusConnection"]
                //};

                //if (!string.IsNullOrEmpty(Configuration["EventBusUserName"]))
                //{
                //    factory.UserName = Configuration["EventBusUserName"];
                //}

                //if (!string.IsNullOrEmpty(Configuration["EventBusPassword"]))
                //{
                //    factory.Password = Configuration["EventBusPassword"];
                //}

                //var retryCount = 5;
                //if (!string.IsNullOrEmpty(Configuration["EventBusRetryCount"]))
                //{
                //    retryCount = int.Parse(Configuration["EventBusRetryCount"]);
                //}



                //////////////////////////////////
                //var settings = sp.GetRequiredService<IOptions<CatalogSettings>>().Value;
                var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

                var factory = new ConnectionFactory()
                {
                    HostName = "localhost",// configuration["EventBusConnection"],
                    UserName = "guest",
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

            RegisterEventBus(services);

            var container = new ContainerBuilder();
            container.Populate(services);

            return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

            ConfigureEventBus(app);
        }

        private void RegisterEventBus(IServiceCollection services)
        {
            var subscriptionClientName = "Customer";

          
                services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
                {
                    var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                    var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                    var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                    var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                    var retryCount = 5;
                    //if (!string.IsNullOrEmpty(Configuration["EventBusRetryCount"]))
                    //{
                    //    retryCount = int.Parse(Configuration["EventBusRetryCount"]);
                    //}

                    return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, iLifetimeScope, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
                });
            

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            services.AddTransient<CustomerCreatedIntergrationEventHandler>();
        }

        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

            eventBus.Subscribe<CustomerCreatedIntegrationEvent, CustomerCreatedIntergrationEventHandler>();
            
        }
    }
}
