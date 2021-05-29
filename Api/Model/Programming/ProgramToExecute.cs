namespace ExploreAspNetCore.Model.Programming
{
    /// <summary>
    /// Modèle pour envoyer du code à interpreter à l'api.
    /// </summary>
    public class ProgramToExecute
    {
        /// <summary>
        /// L'id de session de l'interpreteur
        /// </summary>
        public string? SessionId { get; set; }

        /// <summary>
        /// Le programme à interpreter
        /// </summary>
        public string? Text { get; set; }
    }
}
