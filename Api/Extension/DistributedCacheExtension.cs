using Microsoft.Extensions.DependencyInjection;
using System;
using static ExploreAspNetCore.Properties.Constantes.AstNetCoreEnvironnement;
using static ExploreAspNetCore.Properties.Constantes.RedisEnvironnement;

namespace ExploreAspNetCore.Extension
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

        /// <summary>
        /// Classe pour représenter un identificant unique
        /// </summary>
        public class UniqueIdentifier
        {
            /// <summary>
            /// Constructeur par défaut pour initialiser les membres
            /// </summary>
            public UniqueIdentifier()
            {
                Id = Guid.NewGuid().ToString();
                DateTime = DateTime.Now;
            }

            /// <summary>
            /// L'identificant unique
            /// </summary>
            public string Id { get; set; }

            /// <summary>
            /// La date de création de l'identifiant unique
            /// </summary>
            public DateTime DateTime { get; set; }
        }
    }
}
