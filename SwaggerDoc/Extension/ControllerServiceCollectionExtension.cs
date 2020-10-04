using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SwaggerDoc.ActionFilter;
using SwaggerDoc.Controllers;
using SwaggerDoc.Enveloppe;

namespace SwaggerDoc.Extension
{
    /// <summary>
    /// Classe d'extension permettant d'ajouter les controllers avec les configuration nécessaire
    /// </summary>
    public static class ControllerServiceCollectionExtension
    {
        /// <summary>
        /// Ajout des contrôlleurs avec les configurations nécessaire
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCustomController(this IServiceCollection services)
        {
            services.AddControllers(o =>
            {
                o.Filters.Add<ModelStateFeatureActionFilter>(int.MinValue + 8);
                o.Filters.Add<ApiContexteActionFilter>(int.MinValue + 10);
                o.Filters.Add<JournalisationTransactionActionFilter>(int.MinValue + 12);
            })
            .AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                o.SerializerSettings.Converters.Add(new StringEnumConverter());
            })
            .AddFluentValidation(r => r.RegisterValidatorsFromAssemblyContaining<DateArgsValidation>())
            .ConfigureApiBehaviorOptions(o =>
            {
                o.InvalidModelStateResponseFactory = ApiEnveloppeFactory.InvalidModelStateEnveloppeFactory;
            });

            return services;
        }
    }
}
