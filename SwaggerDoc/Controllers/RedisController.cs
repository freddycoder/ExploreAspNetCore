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
        /// Incrémente de 1 la variable 'increment' dans la cache redis
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CallIncrement()
        {
            var incrementAsString = await _cache.GetStringAsync("increment", CancellationToken.None);

            if (string.IsNullOrWhiteSpace(incrementAsString) || int.TryParse(incrementAsString, out var _) == false)
            {
                incrementAsString = "0";
            }

            var increment = int.Parse(incrementAsString) + 1;

            await _cache.SetStringAsync("increment", increment.ToString(), CancellationToken.None);

            return OkEnveloppe(increment);
        }
    }
}
