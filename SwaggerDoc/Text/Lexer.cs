using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerDoc.Text
{
    public class Lexer
    {
        private readonly char _sepatateur;
        private readonly string _text;
        private int _pos;

        public Lexer(char separateur, string text)
        {
            _sepatateur = separateur;
            _text = text;
            _pos = 0;
        }

        public Token GetNextToken()
        {
            if (_pos >= _text.Length) return new Token { TokenType = TokenType.End, Value = "" };

            var sb = new StringBuilder();

            var tokenType = _text[_pos] == _sepatateur ? TokenType.Variable : TokenType.String;

            while (++_pos < _text.Length && _text[_pos] != _sepatateur)
            {
                sb.Append(_text[_pos]);
            }

            return new Token { TokenType = TokenType.Variable, Value = sb.ToString() };
        }
    }
}
