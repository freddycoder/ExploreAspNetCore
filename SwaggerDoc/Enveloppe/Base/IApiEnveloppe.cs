using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerDoc.Enveloppe.Base
{
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
        /// Liste de message relier à la transaction
        /// </summary>
        public List<Message> Messages { get; set; }
    }
}
