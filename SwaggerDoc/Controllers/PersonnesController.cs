using Microsoft.AspNetCore.Mvc;
using SwaggerDoc.Model.Personnes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SwaggerDoc.Message.ApiEnveloppeFactory;

namespace SwaggerDoc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonnesController
    {
        [HttpGet]
        public IActionResult Personne([FromQuery] Personne personne)
        {
            if (personne == default)
            {
                return NotFoundEnveloppe(personne);
            }

            return OkEnveloppe(personne);
        }
    }
}
