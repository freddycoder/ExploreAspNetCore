using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using SwaggerDoc.Enveloppe;
using SwaggerDoc.Model.Programming;
using SwaggerDoc.Services;
using System;
using System.Collections.Generic;
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
        private readonly IDistributedCache _memoryCache;

        /// <summary>
        /// Constructeur d'initialisation avec les dépendances requise pour le contrôlleur
        /// </summary>
        /// <param name="interpretationService"></param>
        /// <param name="memoryCache"></param>
        public ProgrammingController(IInterpretationService interpretationService, IDistributedCache memoryCache)
        {
            _interpretationService = interpretationService;
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// Interpreter un script écrit en français utilisant le package nuget HLHML
        /// </summary>
        /// <param name="text">Les données relative au programme à exécuter</param>
        /// <returns>Le résultat de la sortie standart produit par l'interpreteur</returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ApiEnveloppe<string>))]
        [ProducesResponseType(400, Type = typeof(ApiEnveloppe<object>))]
        public async Task<IActionResult> ExecuterScript([FromBody] ProgramToExecute text)
        {
            try
            {
                return OkEnveloppe(await _interpretationService.Interprete(text.Text ?? string.Empty));
            }
            catch
            {
                return BadRequestEnveloppe(null, ModelState);
            }
        }

        /// <summary>
        /// Obtenir le dictionnaire des termes de HLHML
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ObtenirDictionaireDesTermes()
        {
            var d = new List<EntreeDictionnaire>(HLHML.Dictionnaire.DictionnaireTermeConnue.TermesConnues.Count);
            
            foreach (var t in HLHML.Dictionnaire.DictionnaireTermeConnue.TermesConnues)
            {
                d.Add(new EntreeDictionnaire
                {
                    Cle = t.Key,
                    Terme = t.Value
                });
            }

            return OkEnveloppe(d);
        }
    }

    /// <summary>
    /// Représente un entrée dans le dictionnaire des termes
    /// </summary>
    public class EntreeDictionnaire
    {
        /// <summary>
        /// La clé du terme
        /// </summary>
        public string? Cle { get; set; }

        /// <summary>
        /// Le terme et les informations relié
        /// </summary>
        public HLHML.Dictionnaire.Terme? Terme { get; set; }
    }
}
