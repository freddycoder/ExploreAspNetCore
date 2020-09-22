using System;
using System.Collections.Concurrent;

namespace SwaggerDoc
{
    public class Transaction
    {
        public Guid TransactionId { get; set; }
        public Guid TrackingId { get; set; }
        public DateTime Debut { get; set; }
        public DateTime? Fin { get; set; }
        public long? Durée { get; set; }
        public string Url { get; set; }
        public string RouteDate { get; set; }
        public string QueryString { get; set; }
        public string Body { get; set; }
        public string Reponse { get; set; }
        public object InformationAdditionnel { get; set; }

        public Transaction Clone() => this.MemberwiseClone() as Transaction ?? throw new InvalidOperationException("Clone fail.");
    }

    public class JournalTransaction : ConcurrentDictionary<Guid, Transaction>
    {
    }
}
