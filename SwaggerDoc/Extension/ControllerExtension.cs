using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using OData.Swagger.Services;
using SwaggerDoc.ActionFilter;
using SwaggerDoc.Controllers;
using SwaggerDoc.Enveloppe;
using SwaggerDoc.Formatter;
using System;
using System.Linq;

namespace SwaggerDoc.Extension
{
    /// <summary>
    /// Classe d'extension permettant d'ajouter les controllers avec les configuration nécessaire
    /// </summary>
    public static class ControllerExtension
    {
        /// <summary>
        /// Ajout des contrôlleurs avec les configurations nécessaire
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCustomController(this IServiceCollection services)
        {
            Func<JsonSerializerSettings> jsonSerializerSettings = default;

            services.AddControllers(o =>
            {
                o.Filters.Add<ModelStateFeatureActionFilter>(int.MinValue + 8);
                o.Filters.Add<ApiContexteActionFilter>(int.MinValue + 10);
                o.Filters.Add<JournalisationTransactionActionFilter>(int.MinValue + 12);

                o.OutputFormatters.Add(new XmlFormater());
                o.OutputFormatters.Add(new ApiEnveloppeFormater());
            })
            .AddNewtonsoftJson(o =>
            {
                o.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                o.SerializerSettings.Converters.Add(new StringEnumConverter());
                o.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

                jsonSerializerSettings = () => o.SerializerSettings;
            })
            .AddFluentValidation(r => r.RegisterValidatorsFromAssemblyContaining<DateArgsValidation>())
            .ConfigureApiBehaviorOptions(o =>
            {
                o.InvalidModelStateResponseFactory = ApiEnveloppeFactory.InvalidModelStateEnveloppeFactory;
            });

            services.AddSingleton(sp => jsonSerializerSettings());

            return services;
        }
    }
}
