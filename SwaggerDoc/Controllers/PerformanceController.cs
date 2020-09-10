using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using SwaggerDoc.Model.Personnes;
using SwaggerDoc.Extension;
using SwaggerDoc.Enveloppe;
using System.ComponentModel.DataAnnotations;
using System.Collections.Immutable;

namespace SwaggerDoc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PerformanceController : Controller
    {
        private static readonly Fixture _fixture = new Fixture();
        private static readonly ImmutableList<Personne> PersonneRepo = _fixture.CreateMany<Personne>(1000).ToImmutableList();
        private static readonly Random _rand = new Random();

        [HttpPost("nbFormat")]
        [ProducesResponseType(200, Type = typeof(PerformanceResult))]
        public IActionResult FormatT([Min(1)]int nbFormat)
        {
            var chrono = new Stopwatch();

            chrono.Start();

            for (int i = 0; i < nbFormat; i++) 
            { 
                Properties.Constantes.PersonXml.Format(PersonneRepo[_rand.Next(PersonneRepo.Count)]);
            }

            chrono.Stop();

            return Ok(new ApiEnveloppe
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
