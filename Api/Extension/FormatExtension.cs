using ExploreAspNetCore.Text;
using System.Reflection;
using System.Text;

namespace ExploreAspNetCore.Extension
{
    /// <summary>
    /// Classe d'extension pour formater du texte.
    /// </summary>
    public static class FormatExtension
    {
        /// <summary>
        /// Extension pour formater depuis une chaine de texte et un modèle. Les proppriété seront remplacer par reflexion.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gabarit"></param>
        /// <param name="model"></param>
        /// <param name="separateur"></param>
        /// <param name="bindingFlags"></param>
        /// <returns></returns>
        public static string Format<T>(this string gabarit, T model, char separateur = '$', BindingFlags bindingFlags = BindingFlags.Public)
        {
            if (gabarit is null) return string.Empty;
            var lexer = new Lexer(separateur, gabarit);
            var chaineFinale = new StringBuilder();
            Token token = lexer.GetNextToken();

            while (token.TokenType != TokenType.End)
            {
                if (token.TokenType == TokenType.String)
                {
                    chaineFinale.Append(token.Value);
                }
                else
                {
                    chaineFinale.Append(typeof(T).GetProperty(token.Value, bindingFlags)?.GetValue(model)?.ToString());
                }

                token = lexer.GetNextToken();
            }

            return chaineFinale.ToString();
        }
    }
}
