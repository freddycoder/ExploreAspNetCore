using System.Text;

namespace SwaggerDoc.Properties
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
    }
}
