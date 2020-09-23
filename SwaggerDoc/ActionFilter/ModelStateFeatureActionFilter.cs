using Microsoft.AspNetCore.Mvc.Filters;
using SwaggerDoc.Services;
using System;

namespace SwaggerDoc.ActionFilter
{
    public class ModelStateFeatureActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var state = context.ModelState;
            context.HttpContext.Features.Set(new ModelStateFeature(state));
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
