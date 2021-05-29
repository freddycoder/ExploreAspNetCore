using System;

namespace ExploreAspNetCore.Model
{
    /// <summary>
    /// Context de l'api. Permet de stocker le TransactionId et le TrackingId
    /// </summary>
    public class ApiContexte
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
