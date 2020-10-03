using HLHML;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SwaggerDoc.Services
{
    /// <summary>
    /// Interface offrant une fonctionnaité d'interpretation de <see cref="string"/>
    /// </summary>
    public interface IInterpretationService
    {
        /// <summary>
        /// Interpreter la chaine représentant un programme, une expression, etc.
        /// </summary>
        Task<string> Interprete(string program);
    }

    public class InterpretationService : IInterpretationService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public InterpretationService(IHttpContextAccessor actionContextAccessor)
        {
            _contextAccessor = actionContextAccessor;
        }

        public async Task<string> Interprete(string program)
        {
            try
            {
                return await Task.Run(() =>
                {
                    using var sw = new StringWriter();

                    var interpreteur = new Interpreteur(sw);

                    interpreteur.Interprete(program);

                    return sw.ToString();
                }, CancellationToken.None);
            }
            catch (Exception e)
            {
                var modelstate = _contextAccessor.HttpContext.Features.Get<ModelStateDictionary>();

                modelstate.AddModelError("text", e.Message);

                throw;
            }
        }
    }
}
