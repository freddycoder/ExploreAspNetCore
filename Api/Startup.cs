using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNet.OData.Extensions;
using ExploreAspNetCore.Extension;
using ExploreAspNetCore.Model;
using ExploreAspNetCore.Services;
using static ExploreAspNetCore.Extension.DistributedCacheExtension;
using AutoFixture;
using Microsoft.AspNetCore.Authorization;
using ExploreAspNetCore.Authorization;
using static System.Boolean;
using static System.Environment;
using static System.StringComparison;

namespace ExploreAspNetCore
{
    /// <summary>
    /// Classe de d�marrage de l'api
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Constructeur d'initialisation
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Acc�d�er au configuration de l'application
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCustomController()
                    .AddOData().Services
                    .AddSwagger()
                    .AddScoped<ApiContexte>()
                    .AddSingleton<JournalTransaction>()
                    .AddTransient<IInterpretationService, InterpretationService>()
                    .AddCorsFromEnvironementVairable()
                    .AddDistributedCache()
                    .AddSingleton<UniqueIdentifier>()
                    .AddSingleton<IFixture, Fixture>();

            if (string.Equals(GetEnvironmentVariable("AUTHENTICATION_ENABLE"), FalseString, OrdinalIgnoreCase))
            {
                services.AddSingleton<IAuthorizationHandler, AllowAnonymous>();
            }
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "api/{documentName}/swagger.json";
            });

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "My API V1");
                c.RoutePrefix = "api";
                c.DocumentTitle = "Swagger UI - Swaggerdoc api";
            });

            app.UseRouting();

            app.UseCorsFromEnvironmentVairable();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.EnableDependencyInjection();
                endpoints.Select();
                endpoints.MapControllers();
            });
        }
    }
}
