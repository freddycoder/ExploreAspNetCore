﻿using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SwaggerDoc.Message
{
    /// <summary>
    /// Enveloppe de réponse de l'api
    /// </summary>
    public class ApiEnveloppe
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

        /// <summary>
        /// Donnée utile
        /// </summary>
        public virtual object Result { get; set; }
    }

    /// <summary>
    /// Message d'information d'une transaction
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Id du message
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Code du message
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Sévérité du message
        /// </summary>
        public string Severity { get; set; }
        /// <summary>
        /// Informations sur le message
        /// </summary>
        public string Text { get; set; }
    }
}
