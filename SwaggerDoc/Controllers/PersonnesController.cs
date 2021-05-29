using AutoFixture;
using Microsoft.AspNet.OData;
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
    [Route("api/[controller]")]
    public class PersonnesController : ControllerBase
    {
        private readonly ILogger<PersonnesController> _logger;
        private readonly IFixture _fixture;

        /// <summary>
        /// Constructeur avec les dépendances requise pour le service.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="fixture"></param>
        public PersonnesController(ILogger<PersonnesController> logger, IFixture fixture)
        {
            _logger = logger;
            _fixture = fixture;
        }

        /// <summary>
        /// Obtenir une liste de personne aléatoire
        /// </summary>
        /// <param name="mediaType"></param>
        /// <returns></returns>
        [HttpGet("List")]
        [EnableQuery]
        public IActionResult GetList()
        {
            var person = _fixture.CreateMany<Personne>();

            return Ok(person);
        }

        /// <summary>
        /// Action pour obtenir une personne
        /// </summary>
        /// <param name="personne"></param>
        /// <param name="mediaType">Le media type</param>
        /// <returns></returns>
        [ProducesResponseType(200, Type = typeof(ApiEnveloppe<Personne>))]
        [ProducesResponseType(200, Type = typeof(Personne))]
        [ProducesResponseType(404, Type = typeof(ApiEnveloppe<object>))]
        [HttpGet]
        public IActionResult Personne([FromQuery] Personne personne, [FromHeader(Name = "Accept")] string mediaType)
        {
            if (personne == default)
            {
                return NotFoundEnveloppe(personne);
            }

            personne.Xml = Properties.Constantes.PersonXml.Format(personne, bindingFlags: System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

            _logger.LogDebug("$Xml$".Format(personne, bindingFlags: System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic));

            if (mediaType == "application/json+enveloppe")
            {
                return OkEnveloppe(personne).WithMessage(new Message
                {
                    Code = "Xml",
                    Id = "1",
                    Severity = "Information",
                    Text = "$Xml$".Format(personne, bindingFlags: System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                });
            }

            return OkEnveloppe(personne);
        }

        /// <summary>
        /// Action pour obtenir une personne
        /// </summary>
        /// <param name="prenom"></param>
        /// <returns></returns>
        [HttpGet("FromHeader")]
        [ProducesResponseType(200, Type = typeof(ApiEnveloppe<string>))]
        [ProducesResponseType(404, Type = typeof(ApiEnveloppe<object>))]
        public IActionResult Personne([FromHeader] string prenom)
        {
            var personne = new Personne { Prenom = prenom };

            personne.Xml = Properties.Constantes.PersonXml.Format(personne, bindingFlags: System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

            _logger.LogDebug("$Xml$".Format(personne, bindingFlags: System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic));

            return OkEnveloppe(personne).WithMessage(new Message
            {
                Code = "Xml",
                Id = "1",
                Severity = "Information",
                Text = "$Xml$".Format(personne, bindingFlags: System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
            });
        }

        /// <summary>
        /// Action pour obtenir une personne
        /// </summary>
        /// <returns></returns>
        [HttpPost("FromPOST")]
        [ProducesResponseType(200, Type = typeof(ApiEnveloppe<string>))]
        [ProducesResponseType(404, Type = typeof(ApiEnveloppe<object>))]
        public IActionResult PersonnePOST([FromBody] Personne personne)
        {
            personne.Xml = Properties.Constantes.PersonXml.Format(personne, bindingFlags: System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);

            _logger.LogDebug("$Xml$".Format(personne, bindingFlags: System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic));

            return OkEnveloppe(personne).WithMessage(new Message
            {
                Code = "Xml",
                Id = "1",
                Severity = "Information",
                Text = "$Xml$".Format(personne, bindingFlags: System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
            });
        }

        /// <summary>
        /// Action pour obtenir une personne
        /// </summary>
        /// <returns></returns>
        [HttpPost("WhatCharIsThis")]
        [ProducesResponseType(200, Type = typeof(ApiEnveloppe<string>))]
        [ProducesResponseType(404, Type = typeof(ApiEnveloppe<object>))]
        public IActionResult CharPOST([FromHeader] string yourchar)
        {
            return OkEnveloppe((int)yourchar[0]);
        }
    }
}
