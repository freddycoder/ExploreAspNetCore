using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SwaggerDoc.Services
{
    public class ModelStateFeature
    {
        public ModelStateDictionary ModelState { get; set; }

        public ModelStateFeature(ModelStateDictionary state)
        {
            ModelState = state;
        }
    }
}