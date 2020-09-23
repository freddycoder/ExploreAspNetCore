using HLHML;
using Microsoft.AspNetCore.Http;
using SwaggerDoc.HttpContextFeature;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace SwaggerDoc.Services
{
    public interface IInterpretationService
    {
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
                var actionContextAccessor = _contextAccessor.HttpContext.Features.Get<ModelStateFeature>();

                actionContextAccessor.ModelState.AddModelError("text", e.Message);

                throw;
            }
        }
    }
}
