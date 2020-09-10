using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static SwaggerDoc.Enveloppe.ApiEnveloppeFactory;

namespace SwaggerDoc.Controllers
{
    /// <summary>
    /// Controlleur pour accéder aux prédictions météos
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        /// <summary>
        /// Constructeur d'initialisation de <see cref="WeatherForecast"/>
        /// </summary>
        /// <param name="logger"></param>
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Obtenir les prédictions météos selon une date de début et une date de fin
        /// </summary>
        /// <param name="dateArgs">La date de début et la date de fin</param>
        /// <returns>Une liste avec les prédictions météos selons l'intervalle de date demandé</returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(WeatherForecast[]))]
        public IActionResult Get([FromQuery] DateArgs dateArgs)
        {
            var rng = new Random();

            var forcastResult = Enumerable.Range(0, (dateArgs.End - dateArgs.Begin).Days + 1).Select(index => new WeatherForecast
            {
                Date = dateArgs.Begin.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();

            return OkEnveloppe(forcastResult);
        }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
    }
}
