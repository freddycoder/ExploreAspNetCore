using Newtonsoft.Json;

namespace ExploreAspNetCore.Enveloppe
{
    /// <summary>
    /// Classe représentant une enveloppe de réponse 422
    /// </summary>
    public class ApiEnveloppe422 : ApiEnveloppe<object?>
    {
        /// <inheritdoc />
        [JsonProperty(PropertyName = "erreurs", Order = 4)]
        public override object? Result { get => base.Result; set => base.Result = value; }
    }
}
