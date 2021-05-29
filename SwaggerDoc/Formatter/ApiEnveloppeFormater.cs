using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using SwaggerDoc.Enveloppe;
using SwaggerDoc.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerDoc.Formatter
{
    /// <summary>
    /// Formater pour ajouter l'enveloppe générique des réponses utilisant le mediatype 
    /// application/json+enveloppe
    /// </summary>
    public class ApiEnveloppeFormater : TextOutputFormatter
    {
        /// <summary>
        /// 
        /// </summary>
        public ApiEnveloppeFormater()
        {
            SupportedMediaTypes.Clear();
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/json+enveloppe"));
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
            var apiContexte = serviceProvider.GetRequiredService<ApiContexte>();
            var serializerSettings = serviceProvider.GetRequiredService<JsonSerializerSettings>();
            var buffer = new StringBuilder();

            var enveloppe = new ApiEnveloppe<object>
            {
                TransactionId = apiContexte.TransactionId,
                TrackingId = apiContexte.TrackingId,
                HttpStatusCode = (HttpStatusCode)context.HttpContext.Response.StatusCode,
                Result = context.Object
            };

            buffer.Append(JsonConvert.SerializeObject(enveloppe, serializerSettings));

            return httpContext.Response.WriteAsync(buffer.ToString(), selectedEncoding);
        }
    }
}
