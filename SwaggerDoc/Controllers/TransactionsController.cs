using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SwaggerDoc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionsController : Controller
    {
        private JournalTransaction _journalTransaction;

        public TransactionsController(JournalTransaction journalTransaction)
        {
            _journalTransaction = journalTransaction;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<Transaction>))]
        public IActionResult GetAll()
        {
            return Ok(_journalTransaction.AsEnumerable().Select(kv => kv.Value));
        }

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
