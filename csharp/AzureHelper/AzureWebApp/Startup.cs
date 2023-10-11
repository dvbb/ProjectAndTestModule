using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureWebApp
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
            services.AddControllers();

            // Add swagger configuration
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "Azure Web Application Api Swagger Doc",
                        Description = "Dependency injection sample",
                        Version = "v1"
                    });
            });

            // dependency injection for [IAzure]
            services.TryAddSingleton<IAzure>(builder =>
            {
                string clientId = Environment.GetEnvironmentVariable("CLIENT_ID");
                string clientSecret = Environment.GetEnvironmentVariable("CLIENT_SECRET");
                string tenantId = Environment.GetEnvironmentVariable("TENANT_ID");
                string subscription = Environment.GetEnvironmentVariable("SUBSCRIPTION_ID");

                AzureCredentials creds = new AzureCredentialsFactory().FromServicePrincipal(clientId, clientSecret, tenantId, AzureEnvironment.AzureGlobalCloud);
                IAzure azure = Azure.Authenticate(creds).WithSubscription(subscription);

                return azure;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            // Add swagger middleware
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "AZURE APP SWAGGER DOC");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
