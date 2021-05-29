using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ExploreAspNetCore.ActionFilter
{
    /// <summary>
    /// Action filter ajoutant l'instance de <see cref="ModelStateDictionary"/> dans les 
    /// Features du context Http
    /// </summary>
    public class ModelStateFeatureActionFilter : IActionFilter
    {
        /// <inheritdoc />
        public void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.Features.Set(context.ModelState);
        }

        /// <inheritdoc />
        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
