using Newtonsoft.Json;
using System;
using System.ComponentModel;

namespace ExploreAspNetCore.Controllers
{
    /// <summary>
    /// Modèle de paramètre pour l'action Get de du controlleur WeatherForcast
    /// </summary>
    public class DateArgs
    {
        /// <summary>
        /// Date de début
        /// </summary>
        [DisplayName("Début")]
        public DateTime Begin { get; set; }

        /// <summary>
        /// Date de fin
        /// </summary>
        [DisplayName("Fin")]
        public DateTime End { get; set; }
    }
}