﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SwaggerDoc.Controllers
{
    /// <summary>
    /// Controlleur permettant d'accéder au transaction de l'api
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class TransactionsController : Controller
    {
        private JournalTransaction _journalTransaction;

        /// <summary>
        /// Constructeur par initialisation avec les dépendances requise pour construire le contrôlleur
        /// </summary>
        /// <param name="journalTransaction"></param>
        public TransactionsController(JournalTransaction journalTransaction)
        {
            _journalTransaction = journalTransaction;
        }

        /// <summary>
        /// Ontenir tout les transaction de l'api
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<Transaction>))]
        public IActionResult GetAll()
        {
            return Ok(_journalTransaction.AsEnumerable().Select(kv => kv.Value));
        }

        /// <summary>
        /// Obtenir une transaction selon un TransactionId
        /// </summary>
        /// <param name="id">L'id de la transaction</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Transaction))]
        [ProducesResponseType(400, Type = typeof(BadRequestObjectResult))]
        [ProducesResponseType(404, Type = typeof(NotFoundObjectResult))]
        public IActionResult Get([Required] string id)
        {
            if (Guid.TryParse(id, out var transactionId))
            {
                if (_journalTransaction.TryGetValue(transactionId, out var transaction))
                {
                    return Ok(transaction);
                }
                else
                {
                    return NotFound($"La transaction {transactionId} n'existe pas.");
                }
            }
            else
            {
                return BadRequest("Format d'identifiant de transaction invalide.");
            }
        }
    }
}
