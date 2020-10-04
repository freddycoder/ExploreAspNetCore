using Microsoft.AspNetCore.Mvc;
using static SwaggerDoc.Enveloppe.ApiEnveloppeFactory;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Caching.Distributed;

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

        /// <summary>
        /// Constructeur par initialisation avec les dépendance du contrôlleur
        /// </summary>
        /// <param name="cache"></param>
        public ConfigurationController(IDistributedCache cache)
        {
            _cache = cache;
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
    }
}
