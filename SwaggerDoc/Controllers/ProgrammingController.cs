using Microsoft.AspNetCore.Mvc;
using SwaggerDoc.Enveloppe;
using SwaggerDoc.Model.Programming;
using SwaggerDoc.Services;
using System;
using System.Threading.Tasks;
using static SwaggerDoc.Enveloppe.ApiEnveloppeFactory;

namespace SwaggerDoc.Controllers
{
    /// <summary>
    /// Api de programmation, permet d'interpreteur des scripts écrit en français
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class ProgrammingController : Controller
    {
        private IInterpretationService _interpretationService;

        public ProgrammingController(IInterpretationService interpretationService)
        {
            _interpretationService = interpretationService;
        }

        /// <summary>
        /// Interpreter un script écrit en français utilisant le package nuget HLHML
        /// </summary>
        /// <param name="text">Les données relative au programme à exécuter</param>
        /// <returns>Le résultat de la sortie standart produit par l'interpreteur</returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ApiEnveloppe<string>))]
        [ProducesResponseType(404, Type = typeof(ApiEnveloppe404))]
        public async Task<IActionResult> ExecuterScript([FromBody] ProgramToExecute text)
        {
            try
            {
                return OkEnveloppe(await _interpretationService.Interprete(text.Text));
            }
            catch
            {
                return BadRequestEnveloppe(null, ModelState);
            }
        }
    }
}
