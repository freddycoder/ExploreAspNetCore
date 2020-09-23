using Microsoft.AspNetCore.Mvc;
using SwaggerDoc.Enveloppe;
using SwaggerDoc.Validator.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using static SwaggerDoc.Enveloppe.ApiEnveloppeFactory;

namespace SwaggerDoc.Controllers
{
    /// <summary>
    /// Permet d'obtenir le noms des différents types dans les assemblies
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class TypesController : ControllerBase
    {
        /// <summary>
        /// Permet d'obtenir tout les type de AppDomain.CurrentDomain
        /// </summary>
        /// <param name="name">Le nom du type doit contenir</param>
        /// <returns>Les types trouvés</returns>
        /// <reponse code="200">La liste des types trouvés</reponse>
        /// <reponse code="404">Si aucun type est trouvé</reponse>
        [ProducesResponseType(200, Type = typeof(ApiEnveloppe<List<string>>))]
        [ProducesResponseType(404, Type = typeof(ApiEnveloppe<object>))]
        [HttpGet]
        public IActionResult Types(string name)
            => Enveloppe(AppDomain.CurrentDomain
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
        [ProducesResponseType(200, Type = typeof(ApiEnveloppe<Type>))]
        [ProducesResponseType(404, Type = typeof(ApiEnveloppe404))]
        [HttpGet("{name}")]
        [TypeNameValidator(Order = int.MinValue + 100)]
        public IActionResult Get(string name)
            => Enveloppe(AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .FirstOrDefault(t => t.Name.Equals(name)));

        /// <summary>
        /// Permet d'obtenir tout les type de AppDomain.CurrentDomain en plus de ceux d'AutoMapper
        /// </summary>
        /// <param name="name">Le nom du type doit contenir</param>
        /// <returns></returns>
        [HttpGet("Mappers")]
        [ProducesResponseType(200, Type = typeof(ApiEnveloppe<List<string>>))]
        [ProducesResponseType(404, Type = typeof(ApiEnveloppe404))]
        public IActionResult TypesWithMapper(string name)
            => Enveloppe(AppDomain.CurrentDomain
                .GetAssemblies()
                .Append(typeof(AutoMapper.AdvancedConfiguration).Assembly)
                .SelectMany(a => a.GetTypes())
                .Where(t => t.Name.Contains(name ?? "", StringComparison.OrdinalIgnoreCase))
                .Select(t => t.Name)
                .ToList());

        private IActionResult Enveloppe(IEnumerable<string> result)
        {
            if (result.Any())
            {
                return OkEnveloppe(result);
            }
            else
            {
                return NotFoundEnveloppe(result);
            }
        }

        private IActionResult Enveloppe(object result)
        {
            if (result != default)
            {
                return OkEnveloppe(result);
            }
            else
            {
                return NotFoundEnveloppe(result);
            }
        }
    }
}
