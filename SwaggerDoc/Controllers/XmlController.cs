using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerDoc.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class XmlController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> CreateXml()
        {
            using StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8);

            string myXml = await reader.ReadToEndAsync();

            return Created("/none", myXml);
        }
    }
}
