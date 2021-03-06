﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ExploreAspNetCore.Enveloppe;
using ExploreAspNetCore.Enveloppe.Base;
using ExploreAspNetCore.Model;
using System;

namespace ExploreAspNetCore.ActionFilter
{
    /// <summary>
    /// Action filter pour initialiser le transactionId et le trackingId et l'insérer dans les réponses
    /// </summary>
    public class ApiContexteActionFilter : IActionFilter
    {
        private ApiContexte? apiContexte;

        /// <summary>
        /// Assigne le TrackingId et le TransactionId au ApiContext
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            apiContexte = context.HttpContext.RequestServices.GetService(typeof(ApiContexte)) as ApiContexte;

            if (apiContexte is null)
            {
                throw new TypeLoadException($"Impossible d'obetnir une instance de '{nameof(ApiContexte)}'");
            }

            apiContexte.TrackingId = Guid.NewGuid();
            apiContexte.TransactionId = Guid.NewGuid();
        }

        /// <summary>
        /// Assigne le TrackingId et le TransactionId depuis l'ApiContext
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (apiContexte is null) throw new ApplicationException($"At this point, {nameof(apiContexte)} should not be null.");

            var result = context.Result;

            if (result is ObjectResult objectResult && objectResult.Value is IApiEnveloppe enveloppe)
            {
                enveloppe.TrackingId = apiContexte.TrackingId;
                enveloppe.TransactionId = apiContexte.TransactionId;
                enveloppe.HttpStatusCode = (System.Net.HttpStatusCode)context.HttpContext.Response.StatusCode;
            }
        }
    }
}
