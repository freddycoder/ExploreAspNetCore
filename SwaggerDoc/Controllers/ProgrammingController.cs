using HLHML;
using Microsoft.AspNetCore.Mvc;
using SwaggerDoc.Enveloppe;
using SwaggerDoc.Model.Programming;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
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
                return OkEnveloppe(await InterpreterEtRetournerStdOutScript(text.Text));
            }
            catch (Exception e)
            {
                return BadRequestEnveloppe(e);
            }
        }

        private async Task<string> InterpreterEtRetournerStdOutScript(string text)
        {
            return await Task.Run(() =>
            {
                using var sw = new StringWriter();

                var interpreteur = new Interpreteur(sw);

                interpreteur.Interprete(text);

                return sw.ToString();
            }, CancellationToken.None);
        }
    }
}
