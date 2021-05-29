using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ExploreAspNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class XmlController : Controller
    {
        /// <summary>
        /// Read xml from request body
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<IActionResult> CreateXml()
        {
            using StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8);

            string myXml = await reader.ReadToEndAsync();

            return Created("/none", myXml);
        }
    }
}
