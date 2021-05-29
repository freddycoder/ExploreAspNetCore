using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using ExploreAspNetCore.Validator.Types;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExploreAspNetCore.Controllers
{
    /// <summary>
    /// Permet d'obtenir le noms des différents types dans les assemblies
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TypesController : ControllerBase
    {
        private readonly IDistributedCache _cache;

        /// <summary>
        /// Constructeur par initialisation avec les dépendances requises
        /// </summary>
        /// <param name="cache"></param>
        public TypesController(IDistributedCache cache)
        {
            _cache = cache;
        }

        /// <summary>
        /// Permet d'obtenir tout les type de AppDomain.CurrentDomain
        /// </summary>
        /// <param name="name">Le nom du type doit contenir</param>
        /// <returns>Les types trouvés</returns>
        /// <reponse code="200">La liste des types trouvés</reponse>
        /// <reponse code="404">Si aucun type est trouvé</reponse>
        [ProducesResponseType(200, Type = typeof(List<string>))]
        [ProducesResponseType(404, Type = typeof(object))]
        [HttpGet]
        public IActionResult Types([FromQuery]string name)
            => SetStatusCode(AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(t => t.Name.Contains(name ?? "", StringComparison.OrdinalIgnoreCase))
                .Select(t => t.Name)
                .ToList());

        /// <summary>
        /// Permet d'obtenir les informations de base d'un type. Retourne le prermier type trouvé. Le nom du type doit être identique au nom de la route. Sensible à la casse.
        /// </summary>
        /// <param name="name">Le nom du type doit contenir</param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(Type))]
        [ProducesResponseType(404, Type = typeof(object))]
        [HttpGet("{name}")]
        [TypeNameValidator(Order = int.MinValue + 100)]
        public IActionResult Get([FromRoute] string name)
            => SetStatusCode(AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .FirstOrDefault(t => t.Name.Equals(name)));

        /// <summary>
        /// Permet d'obtenir tout les type de AppDomain.CurrentDomain en plus de ceux d'AutoMapper
        /// </summary>
        /// <param name="name">Le nom du type doit contenir</param>
        /// <returns></returns>
        [HttpGet("Mappers")]
        [ProducesResponseType(200, Type = typeof(List<string>))]
        [ProducesResponseType(404, Type = typeof(object))]
        public IActionResult TypesWithMapper(string name)
            => SetStatusCode(AppDomain.CurrentDomain
                .GetAssemblies()
                .Append(typeof(AutoMapper.AdvancedConfiguration).Assembly)
                .SelectMany(a => a.GetTypes())
                .Where(t => t.Name.Contains(name ?? "", StringComparison.OrdinalIgnoreCase))
                .Select(t => t.Name)
                .ToList());

        /// <summary>
        /// Retourne le type utiliser pour la cache distribué
        /// </summary>
        /// <returns></returns>
        [HttpGet("DistributedCache")]
        public IActionResult GetIDistributedCacheType()
        {
            return Ok(_cache.GetType());
        }

        private IActionResult SetStatusCode(IEnumerable<string> result)
        {
            if (result.Any())
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        private IActionResult SetStatusCode(object? result)
        {
            if (result != default)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }
    }
}
