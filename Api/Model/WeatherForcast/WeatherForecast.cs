using System;

namespace ExploreAspNetCore
{
    /// <summary>
    /// Mod�le public pour les r�ultats de pr�diction m�t�o
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        /// La date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// La temp�rature en celcius
        /// </summary>
        public int TemperatureC { get; set; }

        /// <summary>
        /// La temp�rature en Farenheit
        /// </summary>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        /// <summary>
        /// Informations suppl�mentaire
        /// </summary>
        public string? Summary { get; set; }
    }
}
