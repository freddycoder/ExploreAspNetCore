namespace SwaggerDoc.Text
{
    public class Token
    {
        public Token(TokenType type, string value)
        {
            TokenType = type;
            Value = value;
        }

        public TokenType TokenType { get; }
        public string Value { get; }
    }

    public enum TokenType
    {
        String, Variable, End
    }
}