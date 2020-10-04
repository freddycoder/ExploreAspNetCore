using Microsoft.Extensions.DependencyInjection;
using System;
using static SwaggerDoc.Properties.Constantes.AstNetCoreEnvironnement;
using static SwaggerDoc.Properties.Constantes.RedisEnvironnement;

namespace SwaggerDoc.Extension
{
    /// <summary>
    /// Classe d'extension pour l'ajout de la cache
    /// </summary>
    public static class DistributedCacheExtension
    {
        /// <summary>
        /// Ajout la cache distribué. Si l'environnement est développement, la cache en mémoire sera utilisé.
        /// Sinon ce sera la cache distribué redis.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDistributedCache(this IServiceCollection services)
        {
            var environnement = Environment.GetEnvironmentVariable(NomCle);

            if (string.IsNullOrWhiteSpace(environnement) || environnement == Developement)
            {
                services.AddDistributedMemoryCache();
            }
            else
            {
                services.AddDistributedRedisCache(o =>
                {
                    o.Configuration = Environment.GetEnvironmentVariable(NomCleRedisHostName); ;
                });
            }

            return services;
        }
    }
}
