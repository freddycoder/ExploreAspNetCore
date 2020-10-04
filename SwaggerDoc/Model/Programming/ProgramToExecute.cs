namespace SwaggerDoc.Model.Programming
{
    /// <summary>
    /// Modèle pour envoyer du code à interpreter à l'api.
    /// </summary>
    public class ProgramToExecute
    {
        /// <summary>
        /// Le programme à interpreter
        /// </summary>
        public string? Text { get; set; }
    }
}
