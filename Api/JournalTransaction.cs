﻿using System;
using System.Collections.Concurrent;

namespace ExploreAspNetCore
{
    /// <summary>
    /// Modèle représentant une transaction
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// L'id de la transaction
        /// </summary>
        public Guid TransactionId { get; set; }

        /// <summary>
        /// L'id de tracking
        /// </summary>
        public Guid TrackingId { get; set; }

        /// <summary>
        /// Date de début de la transaction
        /// </summary>
        public DateTime Debut { get; set; }

        /// <summary>
        /// Date de fin de la transaction
        /// </summary>
        public DateTime? Fin { get; set; }

        /// <summary>
        /// Durée de la transaction
        /// </summary>
        public long? Durée { get; set; }

        /// <summary>
        /// Url relié à la transaction
        /// </summary>
        public string? Url { get; set; }

        /// <summary>
        /// Information dans la route
        /// </summary>
        public string? RouteDate { get; set; }

        /// <summary>
        /// Information dans la query string
        /// </summary>
        public string? QueryString { get; set; }

        /// <summary>
        /// Information du body de la requête
        /// </summary>
        public string? Body { get; set; }

        /// <summary>
        /// Réponse sous forme de chaine de texte
        /// </summary>
        public string? Reponse { get; set; }

        /// <summary>
        /// N'importe quel information aditionnel à journaliser
        /// </summary>
        public object? InformationAdditionnel { get; set; }

        /// <summary>
        /// Retourne un clone de l'objet courrant.
        /// </summary>
        /// <returns></returns>
        public Transaction Clone() => (Transaction) this.MemberwiseClone();
    }

    /// <summary>
    /// Repository utilisé pour le journal de transaction
    /// </summary>
    public class JournalTransaction : ConcurrentDictionary<Guid, Transaction>
    {
    }
}
