using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SwaggerDoc.Enveloppe
{
    /// <summary>
    /// Classe représentant une enveloppe de réponse 422
    /// </summary>
    public class ApiEnveloppe422 : ApiEnveloppe
    {
        [JsonProperty(PropertyName = "erreurs", Order = 4)]
        public override object Result { get => base.Result; set => base.Result = value; }
    }
}
