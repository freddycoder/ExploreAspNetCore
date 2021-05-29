using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ExploreAspNetCore.Extension
{
    public static class CorsExtension
    {
        public static IServiceCollection AddCorsFromEnvironementVairable(this IServiceCollection services)
        {
            if (CorsEnable)
            {
                services.AddCors(setup =>
                {
                    setup.AddPolicy("HLHML", policy =>
                    {
                        policy.WithMethods(CorsMethodEnable());
                        policy.WithOrigins(CorsOriginesEnable());
                        policy.WithHeaders(CorsHeadersEnable());
                    });
                });
            }

            return services;
        }

        public static IApplicationBuilder UseCorsFromEnvironmentVairable(this IApplicationBuilder app)
        {
            if (CorsEnable)
            {
                app.UseCors();
            }

            return app;
        }

        private static bool CorsEnable => string.Equals(Environment.GetEnvironmentVariable("CORS_ENABLE"), bool.TrueString, StringComparison.OrdinalIgnoreCase);


        private static string[] CorsMethodEnable()
        {
            var method = Environment.GetEnvironmentVariable("CORS_METHODS_ALLOW");

            if (string.IsNullOrWhiteSpace(method))
            {
                throw new ArgumentException("Invalid environment variable value: CORS_METHOD_ALLOW. The value is null or whitespace.");
            }

            return method.Split(",");
        }

        private static string[] CorsHeadersEnable()
        {
            var method = Environment.GetEnvironmentVariable("CORS_HEADERS_ALLOW");

            if (string.IsNullOrWhiteSpace(method))
            {
                throw new ArgumentException("Invalid environment variable value: CORS_HEADERS_ALLOW. The value is null or whitespace.");
            }

            return method.Split(",");
        }

        private static string[] CorsOriginesEnable()
        {
            var method = Environment.GetEnvironmentVariable("CORS_ORIGINES_ALLOW");

            if (string.IsNullOrWhiteSpace(method))
            {
                throw new ArgumentException("Invalid environment variable value: CORS_ORIGINES_ALLOW. The value is null or whitespace.");
            }

            return method.Split(",");
        }
    }
}
