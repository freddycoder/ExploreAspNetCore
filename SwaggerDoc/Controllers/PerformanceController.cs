using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using AutoFixture;
using SwaggerDoc.Model.Personnes;
using SwaggerDoc.Extension;
using SwaggerDoc.Enveloppe;
using System.Collections.Immutable;

namespace SwaggerDoc.Controllers
{
    /// <summary>
    /// Controlleur pour tester les performances de l'extension string.Format
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PerformanceController : Controller
    {
        private static readonly Fixture _fixture = new Fixture();
        private static readonly ImmutableList<Personne> PersonneRepo = _fixture.CreateMany<Personne>(1000).ToImmutableList();
        private static readonly Random _rand = new Random();

        /// <summary>
        ///  Action pour tester les performances de l'extension string.Format
        /// </summary>
        /// <param name="nbFormat"></param>
        /// <returns></returns>
        [HttpPost("nbFormat")]
        [ProducesResponseType(200, Type = typeof(PerformanceResult))]
        public IActionResult FormatT([Min(1)]int nbFormat)
        {
            var chrono = new Stopwatch();

            chrono.Start();

            for (int i = 0; i < nbFormat; i++) 
            { 
                Properties.Constantes.PersonXml.Format(PersonneRepo[_rand.Next(PersonneRepo.Count)], bindingFlags: System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            }

            chrono.Stop();

            return Ok(new ApiEnveloppe<PerformanceResult>
            {
                Result = new PerformanceResult
                {
                    NbFormat = nbFormat,
                    TotalTime = chrono.ElapsedMilliseconds,
                    Mean = (double)chrono.ElapsedMilliseconds / nbFormat
                }
            });
        }
    }
}
