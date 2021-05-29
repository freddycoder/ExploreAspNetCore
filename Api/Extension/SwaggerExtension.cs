using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using OData.Swagger.Services;
using System;
using System.IO;
using System.Reflection;

namespace ExploreAspNetCore.Extension
{
    /// <summary>
    /// Classe d'extension relier à l'intégration de swagger dans l'api
    /// </summary>
    public static class SwaggerExtension
    {
        /// <summary>
        /// Ajout de swagger
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Explore Asp.Net Core API",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Frédéric Jacques",
                        Email = string.Empty,
                        Url = new Uri("https://github.com/freddycoder"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under unknow license",
                        Url = new Uri("https://example.com/license"),
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddSwaggerGenNewtonsoftSupport();

            services.AddOdataSwaggerSupport();

            return services;
        }
    }
}
