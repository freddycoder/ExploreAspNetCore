using SwaggerDoc.Text;
using System.Reflection;
using System.Text;

namespace SwaggerDoc.Extension
{
    public static class FormatExtension
    {
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
