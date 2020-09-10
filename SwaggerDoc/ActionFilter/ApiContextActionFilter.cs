using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SwaggerDoc.Enveloppe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerDoc.ActionFilter
{
    /// <summary>
    /// Action filter pour initialiser le transactionId et le trackingId et l'insérer dans les réponses
    /// </summary>
    public class ApiContextActionFilter : IActionFilter
    {
        private ApiContext apiContext;

        /// <summary>
        /// Assigne le TrackingId et le TransactionId au ApiContext
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            apiContext = context.HttpContext.RequestServices.GetService(typeof(ApiContext)) as ApiContext;

            if (apiContext is null)
            {
                throw new TypeLoadException($"Impossible d'obetnir une instance de '{nameof(ApiContext)}'");
            }

            apiContext.TrackingId = Guid.NewGuid();
            apiContext.TransactionId = Guid.NewGuid();
        }

        /// <summary>
        /// Assigne le TrackingId et le TransactionId depuis l'ApiContext
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var result = context.Result;

            if (result is ObjectResult objectResult && objectResult.Value is ApiEnveloppe enveloppe)
            {
                enveloppe.TrackingId = apiContext.TrackingId;
                enveloppe.TransactionId = apiContext.TransactionId;
            }
        }
    }
}
