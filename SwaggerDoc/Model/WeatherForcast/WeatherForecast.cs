using System;

namespace SwaggerDoc
{
    /// <summary>
    /// Modèle public pour les réultats de prédiction météo
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        /// La date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// La température en celcius
        /// </summary>
        public int TemperatureC { get; set; }

        /// <summary>
        /// La température en Farenheit
        /// </summary>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        /// <summary>
        /// Informations supplémentaire
        /// </summary>
        public string Summary { get; set; }
    }
}
