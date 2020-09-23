using SwaggerDoc.Enveloppe;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SwaggerDoc.Enveloppe.Base;

namespace SwaggerDoc.Extension
{
    public static class ActionResultExtension
    {
        public static IActionResult WithMessage(this IActionResult actionResult, Message message)
        {
            if (actionResult is null) throw new ArgumentNullException(nameof(actionResult));
            if (message is null) throw new ArgumentNullException(nameof(message));

            if (actionResult is ObjectResult result)
            {
                if (result.Value is IApiEnveloppe enveloppe) 
                {
                    if (enveloppe.Messages == null)
                    {
                        enveloppe.Messages = new List<Message>();
                    }

                    enveloppe.Messages.Add(message);

                    return actionResult;
                }
            }

            throw new InvalidOperationException("Cannot add message if the result doest not contains ApiEnveloppe");
        }
    }
}
