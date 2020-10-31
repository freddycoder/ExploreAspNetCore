using Microsoft.AspNetCore.Mvc;
using static SwaggerDoc.Enveloppe.ApiEnveloppeFactory;
using Microsoft.AspNetCore.Authorization;

namespace SwaggerDoc.Controllers
{
    /// <summary>
    /// Contrôlleur pour tester l'authentification
    /// </summary>
    [ApiController]
    [Route("[controller]/[action]")]
    public class AuthorisationController : Controller
    {
        /// <summary>
        /// Retourne true si autorisé, sinon 403
        /// </summary>
        /// <returns></returns>
        [HttpGet("{nomVariable}")]
        [ProducesResponseType(200, Type = typeof(bool))]
        [Authorize]
        public IActionResult AreYouAuthorise()
        {
            return OkEnveloppe(true);
        }
    }
}
