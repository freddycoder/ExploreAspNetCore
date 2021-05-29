using Microsoft.AspNetCore.Mvc;
using static SwaggerDoc.Enveloppe.ApiEnveloppeFactory;
using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Caching.Distributed;
using static SwaggerDoc.Extension.DistributedCacheExtension;
using System.ComponentModel;
using SwaggerDoc.Enveloppe;
using Microsoft.AspNetCore.Authorization;

namespace SwaggerDoc.Controllers
{
    /// <summary>
    /// Contrôlleur pour obtenir de l'information sur les configurations de l'api
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
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
        /// Retourne la variable d'environnement demandé
        /// </summary>
        /// <returns></returns>
        [HttpGet("{nomVariable}")]
        public IActionResult GetEnvrionnementVariable([DefaultValue("REDIS_HOSTNAME")] string nomVariable)
        {
            var value = Environment.GetEnvironmentVariable(nomVariable);

            if (value is null) return NotFoundEnveloppe(value);

            return OkEnveloppe(value);
        }

        /// <summary>
        /// Assigner une valeur à une variable d'environnement
        /// </summary>
        /// <param name="nomVariable"></param>
        /// <param name="nouvelleValeur"></param>
        /// <returns></returns>
        [HttpPut("{nomVariable}")]
        [Authorize]
        public IActionResult SetEnvironnementVariable(string nomVariable, [FromBody] string? nouvelleValeur)
        {
            Environment.SetEnvironmentVariable(nomVariable, nouvelleValeur);

            return OkEnveloppe(null);
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
