using Microsoft.AspNetCore.Mvc;
using static SwaggerDoc.Enveloppe.ApiEnveloppeFactory;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Caching.Distributed;
using static SwaggerDoc.Extension.DistributedCacheExtension;

namespace SwaggerDoc.Controllers
{
    /// <summary>
    /// Contrôlleur pour obtenir de l'information sur les configurations de l'api
    /// </summary>
    [ApiController]
    [Route("[controller]/[action]")]
    public class ConfigurationController : Controller
    {
        private readonly IDistributedCache _cache;
        private readonly UniqueIdentifier _identifier;

        /// <summary>
        /// Constructeur par initialisation avec les dépendance du contrôlleur
        /// </summary>
        /// <param name="cache"></param>
        /// <param name="identifier">Identifiant unique de l'instance de cet api</param>
        public ConfigurationController(IDistributedCache cache, UniqueIdentifier identifier)
        {
            _cache = cache;
            _identifier = identifier;
        }

        /// <summary>
        /// Retourne la variable d'environnement ASPNETCORE_ENVIRONNEMENT
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAspNetCoreEnvironnement()
        {
            return OkEnveloppe(Environment.GetEnvironmentVariable(Properties.Constantes.AstNetCoreEnvironnement.NomCle) ?? string.Empty);
        }

        /// <summary>
        /// Retourne le type utiliser pour la cache distribué
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetIDistributedCacheType()
        {
            return OkEnveloppe(_cache.GetType());
        }

        /// <summary>
        /// Permet d'obtenir l'identifiant unique de cet instance de l'api
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetIdentifier()
        {
            return OkEnveloppe(_identifier);
        }
    }
}
