using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SwaggerDoc.Enveloppe;
using SwaggerDoc.Extension;
using SwaggerDoc.Model.Personnes;
using static SwaggerDoc.Enveloppe.ApiEnveloppeFactory;

namespace SwaggerDoc.Controllers
{
    /// <summary>
    /// Controller pour obtenir des personnes
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class PersonnesController
    {
        private ILogger<PersonnesController> _logger;

        /// <summary>
        /// Constructeur avec les dépendances requise pour le service.
        /// </summary>
        /// <param name="logger"></param>
        public PersonnesController(ILogger<PersonnesController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Action pour obtenir une personne
        /// </summary>
        /// <param name="personne"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiEnveloppe<Personne>))]
        [ProducesResponseType(404, Type = typeof(ApiEnveloppe<object>))]
        public IActionResult Personne([FromQuery] Personne personne)
        {
            if (personne == default)
            {
                return NotFoundEnveloppe(personne);
            }

            personne.Xml = Properties.Constantes.PersonXml.Format(personne, bindingFlags: System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

            _logger.LogDebug("$Xml$".Format(personne, bindingFlags: System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic));

            return OkEnveloppe(personne).WithMessage(new Message 
            { 
                Code = "Xml", Id = "1", 
                Severity = "Information", 
                Text = "$Xml$".Format(personne, bindingFlags: System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic) 
            });
        }
    }
}
