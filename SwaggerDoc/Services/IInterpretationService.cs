using HLHML;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using SwaggerDoc.Model.Programming;
using System;
using System.Collections.Concurrent;
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
            var logger = _contextAccessor.HttpContext.RequestServices.GetService(typeof(ILogger<InterpretationService>)) as ILogger<InterpretationService>;

            if (logger is null)
            {
                throw new InvalidProgramException("ILogger is not register correclty");
            }

            try
            {
                int timeout = 2000;

                var task = Task.Run(() =>
                {
                    using var sw = new StringWriter();

                    var interpreteur = new Interpreteur(sw);

                    interpreteur.Interprete(program.Text);

                    return sw.ToString();
                }, CancellationToken.None);

                if (await Task.WhenAny(task, Task.Delay(timeout)) == task)
                {
                    return task.Result;
                }
                else
                {
                    try
                    {
                        task.Dispose();
                    }
                    catch (Exception e)
                    {
                        logger.LogError(new EventId(546, "HLHML Task Dispose Exception"), e, "Exception was throwed when try to dispose HLHML interpretation Task");
                    }
                    
                    throw new TimeoutException("You program passed the timeout of 2 seconds...");
                }
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
