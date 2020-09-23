using SwaggerDoc.Text;
using System.Reflection;
using System.Text;

namespace SwaggerDoc.Extension
{
    public static class FormatExtension
    {
        public static readonly BindingFlags BindingFlags = BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public;

        public static string Format<T>(this string gabarit, T model, char separateur = '$')
        {
            if (gabarit is null) return null;
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
                    chaineFinale.Append(typeof(T).GetProperty(token.Value, BindingFlags)?.GetValue(model).ToString());
                }

                token = lexer.GetNextToken();
            }

            return chaineFinale.ToString();
        }
    }
}
