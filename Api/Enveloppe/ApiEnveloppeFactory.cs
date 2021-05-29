using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace ExploreAspNetCore.Enveloppe
{
    /// <summary>
    /// Classe static offrant des méthodes pour générer des ApiEnveloppe
    /// </summary>
    public static class ApiEnveloppeFactory
    {
        /// <summary>
        /// Méthode pour générer les ApiEnveloppe de type 422
        /// </summary>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        public static IActionResult InvalidModelStateEnveloppeFactory(ActionContext actionContext)
        {
            var result = new UnprocessableEntityObjectResult(new ApiEnveloppe422
            {
                Messages = new List<Message>
                {
                    new Message
                    {
                        Id = "API_001",
                        Code = "422",
                        Severity = "1",
                        Text = "Une ou plusieurs erreurs de validiton sont survenues"
                    }
                },
                Result = new ValidationProblemDetails(actionContext.ModelState).Errors
            });

            return result;
        }

        /// <summary>
        /// Un envloppe avec le status 200
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static IActionResult OkEnveloppe(object? result)
        {
            var envloppeResult = new OkObjectResult(new ApiEnveloppe<object>
            {
                Result = result
            });

            return envloppeResult;
        }

        /// <summary>
        /// Un enveloppe avec le status 404
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static IActionResult NotFoundEnveloppe(object? result)
        {
            var envloppeResult = new NotFoundObjectResult(new ApiEnveloppe<object>
            {
                Messages = new List<Message>
                {
                    new Message
                    {
                        Id = "API_002",
                        Code = "404",
                        Severity = "Minor",
                        Text = "Aucun résultat trouvé"
                    }
                }
            });

            return envloppeResult;
        }

        /// <summary>
        /// Un enveloppe avec le status 400. La propriété message sera populé par le ModeleStateDictinary
        /// </summary>
        /// <param name="result"></param>
        /// <param name="modelState"></param>
        /// <returns></returns>
        public static IActionResult BadRequestEnveloppe(object? result, ModelStateDictionary modelState)
        {
            var apiEnveloppe = new ApiEnveloppe<object?>
            {
                Messages = new List<Message>(),
                Result = result
            };

            foreach (var state in modelState)
            {
                foreach (var erreur in state.Value.Errors)
                {
                    apiEnveloppe.Messages.Add(new Message
                    {
                        Id = state.Key,
                        Code = "API_POST_TRAIT",
                        Severity = "Error",
                        Text = erreur.ErrorMessage
                    });
                }
            }

            var envloppeResult = new BadRequestObjectResult(apiEnveloppe);

            return envloppeResult;
        }
    }
}
