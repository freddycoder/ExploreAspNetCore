using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace ExploreAspNetCore.Validator.Types
{
    /// <summary>
    /// Permet d'intercepter les paramètres de la route et de valider le paramètre 'name' pour qu'il respecte les formats des noms
    /// des types en c#.
    /// </summary>
    public class TypeNameValidatorAttribute : ActionFilterAttribute
    {
        /// <inheritdoc />
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var name = context.RouteData.Values["name"] as string;

            if (name.Any(c => char.IsWhiteSpace(c)))
            {
                context.ModelState.AddModelError("name", "Le nom de peut pas contenir d'esapce");
            }

            if (name.All(c => char.IsDigit(c)))
            {
                context.ModelState.AddModelError("name", "Le nom ne peut pas être seulement des nombres");
            }
        }
    }
}
