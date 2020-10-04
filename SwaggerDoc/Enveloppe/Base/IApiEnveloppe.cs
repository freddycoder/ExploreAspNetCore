using System;
using System.Collections.Generic;
using System.Net;

namespace SwaggerDoc.Enveloppe.Base
{
    /// <summary>
    /// Interface représentant une ApiEnveloppe
    /// </summary>
    public interface IApiEnveloppe
    {
        /// <summary>
        /// L'id de transaction
        /// </summary>
        public Guid TransactionId { get; set; }

        /// <summary>
        /// L'id de corréaltion de tracking
        /// </summary>
        public Guid TrackingId { get; set; }

        /// <summary>
        /// Le code Http
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; set; }

        /// <summary>
        /// Liste de message relier à la transaction
        /// </summary>
        public List<Message>? Messages { get; set; }
    }
}
