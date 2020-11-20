using HLHML;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SwaggerDoc.Model.Programming;
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
        Task<string> Interprete(ProgramToExecute program);
    }

    /// <summary>
    /// Implémentation de <see cref="IInterpretationService"/> utilisant le package nuget HLHML pour interpreter le programme.
    /// </summary>
    public class InterpretationService : IInterpretationService
    {
        private readonly IHttpContextAccessor _contextAccessor;

        /// <summary>
        /// Constructeur par initialisation avec les dépendances requise pour l'execution du services.
        /// </summary>
        /// <param name="actionContextAccessor"></param>
        public InterpretationService(IHttpContextAccessor actionContextAccessor)
        {
            _contextAccessor = actionContextAccessor;
        }

        /// <inheritdoc />
        public async Task<string> Interprete(ProgramToExecute program)
        {
            try
            {
                return await Task.Run(() =>
                {
                    //if (string.IsNullOrWhiteSpace(text.SessionId) == false)
                    //{
                    //    var variables = _cache.Get(text.SessionId);

                    //    var scope = new HLHML.Scope(variables);
                    //}

                    using var sw = new StringWriter();

                    var interpreteur = new Interpreteur(sw);

                    interpreteur.Interprete(program.Text);

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
