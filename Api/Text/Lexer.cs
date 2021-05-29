using System.Text;

namespace ExploreAspNetCore.Text
{
    /// <summary>
    /// Lexer pour parser du texte plus simplement.
    /// </summary>
    public class Lexer
    {
        private readonly char _sepatateur;
        private readonly string _text;
        private int _pos;

        /// <summary>
        /// Construire un nouveau lexer selon un séparateur et du text.
        /// </summary>
        /// <param name="separateur"></param>
        /// <param name="text"></param>
        public Lexer(char separateur, string text)
        {
            _sepatateur = separateur;
            _text = text;
            _pos = 0;
        }

        /// <summary>
        /// Permet d'obtenir le prochain jeton.
        /// </summary>
        public Token GetNextToken()
        {
            if (_pos >= _text.Length) return new Token(TokenType.End, string.Empty);

            var sb = new StringBuilder();

            var tokenType = _text[_pos] == _sepatateur ? TokenType.Variable : TokenType.String;

            while (++_pos < _text.Length && _text[_pos] != _sepatateur)
            {
                sb.Append(_text[_pos]);
            }

            return new Token(tokenType, sb.ToString());
        }
    }
}
