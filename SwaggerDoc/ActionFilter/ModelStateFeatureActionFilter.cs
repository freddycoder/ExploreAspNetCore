using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SwaggerDoc.HttpContextFeature;

namespace SwaggerDoc.ActionFilter
{
    public class ModelStateFeatureActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            ModelStateDictionary state = context.ModelState;
            context.HttpContext.Features.Set(new ModelStateFeature(state));
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
