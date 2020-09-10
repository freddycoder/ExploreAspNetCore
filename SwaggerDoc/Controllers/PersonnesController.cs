using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SwaggerDoc.Enveloppe;
using SwaggerDoc.Extension;
using SwaggerDoc.Model.Personnes;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
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
        public IActionResult Personne([FromQuery] Personne personne)
        {
            if (personne == default)
            {
                return NotFoundEnveloppe(personne);
            }

            personne.Xml = Properties.Constantes.PersonXml.Format(personne);

            _logger.LogDebug("$Xml$".Format(personne));

            return OkEnveloppe(personne).WithMessage(new Message { Code = "Xml", Id = "1", Severity = "Information", Text = "$Xml$".Format(personne) });
        }
    }
}
