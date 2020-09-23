namespace SwaggerDoc.Text
{
    public class Token
    {
        public TokenType TokenType;
        public string Value;
    }

    public enum TokenType
    {
        String, Variable, End
    }
}