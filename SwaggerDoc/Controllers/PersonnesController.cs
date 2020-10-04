using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SwaggerDoc.Enveloppe;
using SwaggerDoc.Extension;
using SwaggerDoc.Model.Personnes;
using static SwaggerDoc.Enveloppe.ApiEnveloppeFactory;

namespace SwaggerDoc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonnesController
    {
        private ILogger<PersonnesController> _logger;

        public PersonnesController(ILogger<PersonnesController> logger)
        {
            _logger = logger;
        }

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
