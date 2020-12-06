using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using SwaggerDoc.Enveloppe;
using SwaggerDoc.Model.Programming;
using SwaggerDoc.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using static SwaggerDoc.Enveloppe.ApiEnveloppeFactory;

namespace SwaggerDoc.Controllers
{
    /// <summary>
    /// Api de programmation, permet d'interpreteur des scripts écrit en français
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProgrammingController : Controller
    {
        private IInterpretationService _interpretationService;
        private readonly IDistributedCache _cache;

        /// <summary>
        /// Constructeur d'initialisation avec les dépendances requise pour le contrôlleur
        /// </summary>
        /// <param name="interpretationService"></param>
        /// <param name="memoryCache"></param>
        public ProgrammingController(IInterpretationService interpretationService, IDistributedCache memoryCache)
        {
            _interpretationService = interpretationService;
            _cache = memoryCache;
        }

        /// <summary>
        /// Interpreter un script écrit en français utilisant le package nuget HLHML
        /// </summary>
        /// <param name="programToExecute">Les données relative au programme à exécuter</param>
        /// <returns>Le résultat de la sortie standart produit par l'interpreteur</returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ApiEnveloppe<string>))]
        [ProducesResponseType(400, Type = typeof(ApiEnveloppe<object>))]
        public async Task<IActionResult> ExecuterScript([FromBody] ProgramToExecute programToExecute)
        {
            try
            {
                return OkEnveloppe(await _interpretationService.Interprete(programToExecute));
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
            var d = new List<EntreeDictionnaire>(HLHML.DictionnaireTermeConnue.TermesConnues.Count);
            
            foreach (var t in HLHML.DictionnaireTermeConnue.TermesConnues)
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
        public HLHML.Terme? Terme { get; set; }
    }
}
