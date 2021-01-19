using AutoMapper;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SwaggerDoc
{
    /// <summary>
    /// Repository utilisé pour le journal de transaction
    /// </summary>
    public class JournalTransaction : IEnumerable<KeyValuePair<Guid, Transaction>>
    {
        private readonly ConcurrentDictionary<Guid, Trx> _database;
        private readonly IMapper _mapper;

        public JournalTransaction(IMapper mapper)
        {
            _mapper = mapper;
            _database = new ConcurrentDictionary<Guid, Trx>();
        }

        public bool TryAdd(Guid transactionId, Transaction transaction)
        {
            if (transaction == default) throw new ArgumentNullException(nameof(transaction));
            if (transactionId != transaction.TransactionId) throw new ArgumentException("TransactionId parameter must match transaction.TransactionId property.");

            var trx = _mapper.Map<Trx>(transaction);

            Debug.Assert(trx != default);

            return _database.TryAdd(transactionId, trx);
        }

        public bool TryGetValue<TransactionType>(Guid transactionId, out TransactionType transaction) where TransactionType : Transaction
        {
            var r = _database.TryGetValue(transactionId, out var trx);

            transaction = _mapper.Map<TransactionType>(trx);

            return r;
        }

        public bool TryUpdate<TransactionType>(Guid transactionId, TransactionType newTrx, TransactionType comparaison) where TransactionType : Transaction
        {
            var n = _mapper.Map<Trx>(newTrx);
            var c = _mapper.Map<Trx>(comparaison);

            return _database.TryUpdate(transactionId, n, c);
        }

        public IEnumerator<KeyValuePair<Guid, Transaction>> GetEnumerator()
        {
            foreach (var kv in _database)
            {
                yield return new KeyValuePair<Guid, Transaction>(kv.Key, _mapper.Map<Transaction>(kv.Value));
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count => _database.Count;
    }
}
