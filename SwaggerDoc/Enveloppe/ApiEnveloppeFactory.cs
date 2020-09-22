using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace SwaggerDoc.Enveloppe
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
        public static IActionResult OkEnveloppe(object result)
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
        public static IActionResult NotFoundEnveloppe(object result)
        {
            var envloppeResult = new NotFoundObjectResult(new ApiEnveloppe404
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
        /// Un enveloppe avec le status 404
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static IActionResult BadRequestEnveloppe(object result)
        {
            var envloppeResult = new NotFoundObjectResult(new ApiEnveloppe404
            {
                Messages = new List<Message>
                {
                    new Message
                    {
                        Id = "API_003",
                        Code = "400",
                        Severity = "Major",
                        Text = "Un erreur est survenu"
                    }
                },
                Result = result
            });

            return envloppeResult;
        }
    }
}
