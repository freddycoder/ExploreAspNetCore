using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using ExploreAspNetCore.Enveloppe;
using ExploreAspNetCore.Enveloppe.Base;
using ExploreAspNetCore.Model;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ExploreAspNetCore.Formatter
{
    /// <summary>
    /// Formater pour ajouter l'enveloppe générique des réponses utilisant le mediatype 
    /// application/json+enveloppe
    /// </summary>
    public class ApiEnveloppeFormater : TextOutputFormatter
    {
        /// <inheritdoc />
        public ApiEnveloppeFormater()
        {
            SupportedMediaTypes.Clear();
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("appliation/json+enveloppe"));
            SupportedEncodings.Add(Encoding.UTF8);
        }

        /// <summary>
        /// Indicate if the serializer can be used or not
        /// </summary>
        /// <inheritdoc />
        public override bool CanWriteResult(OutputFormatterCanWriteContext context)
        {
            return context.ContentType == "application/json+enveloppe";
        }

        /// <inheritdoc />
        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var httpContext = context.HttpContext;
            var serviceProvider = httpContext.RequestServices;

            var logger = serviceProvider.GetRequiredService<ILogger<ApiEnveloppeFormater>>();
            logger.LogInformation("Executing application/json+enveloppe output formatter");

            var option = serviceProvider.GetRequiredService<IOptions<MvcNewtonsoftJsonOptions>>().Value;
            var buffer = new StringBuilder();

            if (context.Object is IApiEnveloppe == false)
            {
                var apiContexte = serviceProvider.GetRequiredService<ApiContexte>();

                var enveloppe = new ApiEnveloppe<object>
                {
                    TransactionId = apiContexte.TransactionId,
                    TrackingId = apiContexte.TrackingId,
                    HttpStatusCode = (HttpStatusCode)httpContext.Response.StatusCode,
                    Result = context.Object
                };

                buffer.Append(JsonConvert.SerializeObject(enveloppe, option.SerializerSettings));
            }
            else
            {
                buffer.Append(JsonConvert.SerializeObject(context.Object, option.SerializerSettings));
            }

            return httpContext.Response.WriteAsync(buffer.ToString(), selectedEncoding);
        }
    }
}
