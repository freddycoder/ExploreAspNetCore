namespace SwaggerDoc.Text
{
    /// <summary>
    /// Représente un jeton d'une chaine de texte plus large
    /// </summary>
    public class Token
    {
        /// <summary>
        /// Construire le jeton
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        public Token(TokenType type, string value)
        {
            TokenType = type;
            Value = value;
        }

        /// <summary>
        /// Le type du jeton
        /// </summary>
        public TokenType TokenType { get; }

        /// <summary>
        /// La valeur du jeton
        /// </summary>
        public string Value { get; }
    }

    /// <summary>
    /// Enumération des type de jeton
    /// </summary>
    public enum TokenType
    {
        /// <summary>
        /// Représente une chaine de texte
        /// </summary>
        String, 

        /// <summary>
        /// Représente une variable
        /// </summary>
        Variable, 

        /// <summary>
        /// Représente la fin d'une chaine de texte
        /// </summary>
        End
    }
}