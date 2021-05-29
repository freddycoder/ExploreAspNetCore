using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExploreAspNetCore.Authorization
{
    /// <summary>
    /// Classe utiliser quand la varaible d'environnement AUTHENTICATION_ENABLE est à "false".
    /// </summary>
    public class AllowAnonymous : IAuthorizationHandler
    {
        /// <inheritdoc />
        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            foreach (var requirement in context.PendingRequirements.ToArray())
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
