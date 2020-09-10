using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerDoc.Properties
{
    public static class Constantes
    {
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
