using IdentityServer4.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using static Identity.Api.AppConfig;

namespace Identity.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddIdentityServer()
                        .AddDeveloperSigningCredential()
                        .AddInMemoryClients(ApiConfig.GetClients())
                        .AddInMemoryApiResources(ApiConfig.GetApiResources());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseIdentityServer();

            app.UseMvc();
        }
    }

    public static class AppConfig
    {
        public static class ApiConfig
        {

            public static IEnumerable<ApiResource> GetApiResources()
            {
                return new[]
                {
                new ApiResource("OcelotApi", "OcelotApi")
            };
            }

            public static IEnumerable<Client> GetClients()
            {
                return new[]
                {
                new Client
                {
                    ClientId = "chathuranga",
                    ClientSecrets = new [] { new Secret("sandun".Sha256()) },
                    AllowedGrantTypes = IdentityServer4.Models.GrantTypes.ClientCredentials,
                    AllowedScopes = new [] { "OcelotApi" }
                }
            };
            }
        }
    }
}
