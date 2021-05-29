using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Threading;
using System.Threading.Tasks;
using static SwaggerDoc.Enveloppe.ApiEnveloppeFactory;

namespace SwaggerDoc.Controllers
{
    /// <summary>
    /// Controller pour interagir avec la cache redis
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class RedisController : Controller
    {
        private readonly IDistributedCache _cache;

        /// <summary>
        /// Controller par initialisation avec les dpendance requise pour le contrôlleur
        /// </summary>
        /// <param name="cache"></param>
        public RedisController(IDistributedCache cache)
        {
            _cache = cache;
        }

        /// <summary>
        /// Permet d'obtenir le nombres de likes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(string))]
        public async Task<IActionResult> GetLikes()
        {
            var likes = await _cache.GetStringAsync("increment", CancellationToken.None);

            if (string.IsNullOrWhiteSpace(likes) || int.TryParse(likes, out var _) == false)
            {
                likes = "0";
            }

            return Ok(likes);
        }

        /// <summary>
        /// Incrémente de 1 la variable 'increment' dans la cache redis
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(string))]
        public async Task<IActionResult> CallIncrement()
        {
            var incrementAsString = await _cache.GetStringAsync("increment", CancellationToken.None);

            if (string.IsNullOrWhiteSpace(incrementAsString) || int.TryParse(incrementAsString, out var _) == false)
            {
                incrementAsString = "0";
            }

            var increment = int.Parse(incrementAsString) + 1;

            await _cache.SetStringAsync("increment", increment.ToString(), CancellationToken.None);

            return Ok(increment);
        }
    }
}
