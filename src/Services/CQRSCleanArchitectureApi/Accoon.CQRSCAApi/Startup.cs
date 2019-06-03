using Accoon.CQRSCAApi.Application.Interfaces;
using Accoon.CQRSCAApi.Application.UserCases.CreateCustomer;
using Accoon.CQRSCLApi.Infastructure;
using Accoon.CQRSCLApi.Persistence;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
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
            public void ConfigureServices(IServiceCollection services)
        {

            // register db context and migration assebly
            var connectionString = Configuration.GetConnectionString("CQRSCADbContext").ToString();
            services.AddDbContext<CqrscaDbContext>
                (options => options.UseSqlServer(connectionString, x => x.MigrationsAssembly("Accoon.CQRSCLApi.Domain")));

            // add mvc             
            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_2);

            // swagger 
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

            services.AddMediatR(typeof(CreateCustomerHandler).GetTypeInfo().Assembly);
            services.AddTransient<INotificationService, NotificationService>();

            
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
}
