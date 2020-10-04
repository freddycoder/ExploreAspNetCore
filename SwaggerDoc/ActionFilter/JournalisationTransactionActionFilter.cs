using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SwaggerDoc.Model;
using System;
using System.IO.Pipelines;
using System.Text;

namespace SwaggerDoc.ActionFilter
{
    /// <summary>
    /// Action filter permettant de journliser les appels à l'api. En journalisant les intrants et les extrants
    /// </summary>
    public class JournalisationTransactionActionFilter : IActionFilter
    {
        private JournalTransaction? _journalTransaction;
        private ApiContexte? _apiContexte;
        private ILogger<JournalisationTransactionActionFilter>? _logger;

        /// <inheritdoc />
        public void OnActionExecuting(ActionExecutingContext context)
        {
            _logger = context.HttpContext.RequestServices.GetService(typeof(ILogger<JournalisationTransactionActionFilter>)) as ILogger<JournalisationTransactionActionFilter>;
            _journalTransaction = context.HttpContext.RequestServices.GetService(typeof(JournalTransaction)) as JournalTransaction;
            _apiContexte = context.HttpContext.RequestServices.GetService(typeof(ApiContexte)) as ApiContexte;

            if (_apiContexte is null) throw new ApplicationException($"At this point, {nameof(_apiContexte)} should not be null.");
            if (_journalTransaction is null) throw new ApplicationException($"At this point, {nameof(_journalTransaction)} should not be null.");

            var transaction = new Transaction
            {
                Body = GetBody(context.HttpContext.Request),
                Debut = DateTime.Now,
                QueryString = context.HttpContext.Request.QueryString.ToString(),
                RouteDate = GetRouteData(context.HttpContext.Request.RouteValues),
                TrackingId = _apiContexte.TrackingId,
                TransactionId = _apiContexte.TransactionId,
                Url = context.HttpContext.Request.GetDisplayUrl()
            };

            var result = _journalTransaction.TryAdd(_apiContexte.TransactionId, transaction);

            if (result == false)
            {
                _logger.LogError($"Impossible d'ajouter la transaction {_apiContexte.TransactionId} au journal de transaction");
            }
        }

        /// <inheritdoc />
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger = context.HttpContext.RequestServices.GetService(typeof(ILogger<JournalisationTransactionActionFilter>)) as ILogger<JournalisationTransactionActionFilter>;
            _journalTransaction = context.HttpContext.RequestServices.GetService(typeof(JournalTransaction)) as JournalTransaction;
            _apiContexte = context.HttpContext.RequestServices.GetService(typeof(ApiContexte)) as ApiContexte;

            if (_apiContexte is null) throw new ApplicationException($"At this point, {nameof(_apiContexte)} should not be null.");
            if (_journalTransaction is null) throw new ApplicationException($"At this point, {nameof(_journalTransaction)} should not be null.");

            if (_journalTransaction.TryGetValue(_apiContexte.TransactionId, out var transactionOrigin))
            {
                var transaction = transactionOrigin.Clone();

                transaction.Fin = DateTime.Now;
                transaction.Durée = (transaction.Fin - transaction.Debut).Value.Ticks;
                transaction.Reponse = JsonConvert.SerializeObject(context.Result);

                var result = _journalTransaction.TryUpdate(_apiContexte.TransactionId, transaction, transactionOrigin);

                if (result == false)
                {
                    _logger.LogError($"Impossible de mettre à jour la transaction {_apiContexte.TransactionId} au journal de transaction. Aucun changement, ou erreur lors de la mise a jour.");
                }
            }
            else
            {
                _logger.LogError($"Impossible de mettre à jour la transaction {_apiContexte.TransactionId} au journal de transaction. La transaction n'est pas dans le journal.");
            }
        }

        private string GetRouteData(RouteValueDictionary routeValues)
        {
            var sb = new StringBuilder();

            foreach (var keyPair in routeValues)
            {
                sb.Append($"{keyPair}");
            }

            return sb.ToString();
        }

        private string GetBody(HttpRequest request)
        {
            if (request.BodyReader.TryRead(out ReadResult readResult))
            {
                return readResult.ToString() ?? string.Empty;
            }

            return string.Empty;
        }
    }
}
