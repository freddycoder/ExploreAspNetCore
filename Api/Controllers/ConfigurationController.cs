using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Caching.Distributed;
using static ExploreAspNetCore.Extension.DistributedCacheExtension;
using System.ComponentModel;
using Microsoft.AspNetCore.Authorization;

namespace ExploreAspNetCore.Controllers
{
    /// <summary>
    /// Contrôlleur pour obtenir de l'information sur les configurations de l'api
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ConfigurationController : Controller
    {
        private readonly UniqueIdentifier _identifier;

        /// <summary>
        /// Constructeur par initialisation avec les dépendance du contrôlleur
        /// </summary>
        /// <param name="identifier">Identifiant unique de l'instance de cet api</param>
        public ConfigurationController(UniqueIdentifier identifier)
        {
            _identifier = identifier;
        }

        /// <summary>
        /// Retourne la variable d'environnement demandé
        /// </summary>
        /// <returns></returns>
        [HttpGet("{nomVariable}")]
        [ProducesResponseType(200, Type = typeof(string))]
        public IActionResult GetEnvrionnementVariable([DefaultValue("REDIS_HOSTNAME")] string nomVariable)
        {
            var value = Environment.GetEnvironmentVariable(nomVariable);

            if (value is null) return NotFound(value);

            return Ok(value);
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

            return Ok(null);
        }

        /// <summary>
        /// Permet d'obtenir l'identifiant unique de cet instance de l'api
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(UniqueIdentifier))]
        public IActionResult GetIdentifier()
        {
            return Ok(_identifier);
        }
    }
}
