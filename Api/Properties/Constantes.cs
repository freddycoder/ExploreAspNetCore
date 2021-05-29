using System.Text;

namespace ExploreAspNetCore.Properties
{
    /// <summary>
    /// Classe de constantes
    /// </summary>
    public static class Constantes
    {
        /// <summary>
        /// Exemple d'une personne séréaliser en xml.
        /// </summary>
        public static readonly string PersonXml = InitPersonXml();

        private static string InitPersonXml()
        {
            var sb = new StringBuilder();

            sb.Append("<person>");

            sb.Append("  <prenom>$Prenom$</prenom>");
            sb.Append("  <nom>$Nom$</nom>");
            sb.Append("  <dateNaissance>$DateNaissance$</dateNaissance>");
            sb.Append("  <genre>$Genre$</genre>");

            sb.Append("</person>");

            return sb.ToString();
        }

        /// <summary>
        /// Classe de constantes relié à la varaible d'environnement ASPNETCORE_ENVIRONMENT
        /// </summary>
        public static class AstNetCoreEnvironnement
        {
            /// <summary>
            /// Nom de la clé de la variable d'environnement ASPNETCORE_ENVIRONMENT
            /// </summary>
            public const string NomCle = "ASPNETCORE_ENVIRONMENT";

            /// <summary>
            /// Nom de l'environnement lorsque la variable représente l'environnement Development
            /// </summary>
            public const string Developement = "Development";
        }
        /// <summary>
        /// Classe de constantes relié à la varaible d'environnement ASPNETCORE_ENVIRONMENT
        /// </summary>
        public static class RedisEnvironnement
        {
            /// <summary>
            /// Nom de la clé de la variable d'environnement REDIS_HOSTNAME
            /// </summary>
            public const string NomCleRedisHostName = "REDIS_HOSTNAME";
        }
    }
}
