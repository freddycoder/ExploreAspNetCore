using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerDoc.ActionFilter
{
    /// <summary>
    /// Context de l'api. Permet de stocker le TransactionId et le TrackingId
    /// </summary>
    public class ApiContext
    {
        /// <summary>
        /// L'id de la transaction
        /// </summary>
        public Guid TransactionId { get; set; }

        /// <summary>
        /// L'id de tracking
        /// </summary>
        public Guid TrackingId { get; set; }
    }
}
