using SwaggerDoc.Enveloppe;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SwaggerDoc.Enveloppe.Base;

namespace SwaggerDoc.Extension
{
    /// <summary>
    /// Permet d'ajouter un message à l'interieur d'un apiEnveloppe qui est à l'intérieur d'un IActionResult.
    /// </summary>
    public static class ActionResultExtension
    {
        /// <summary>
        /// ermet d'ajouter des messages à l'interieur d'un apiEnveloppe qui est à l'intérieur d'un IActionResult.
        /// </summary>
        /// <param name="actionResult"></param>
        /// <param name="message">Le message à ajouter</param>
        /// <returns></returns>
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
