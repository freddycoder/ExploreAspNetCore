using System;

namespace SwaggerDoc
{
    /// <summary>
    /// Modèle représentant une transaction
    /// </summary>
    public class TransactionDerive : Transaction
    {
        public string Miscellaneous { get; set; }

        /// <summary>
        /// Retourne un clone de l'objet courrant.
        /// </summary>
        /// <returns></returns>
        public new TransactionDerive Clone() => (TransactionDerive) this.MemberwiseClone();
    }
}
