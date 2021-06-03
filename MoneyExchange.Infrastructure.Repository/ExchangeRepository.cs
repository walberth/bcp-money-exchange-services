namespace MoneyExchange.Infrastructure.Repository
{
    using Entity;
    using Interfaces;
    using System.Data;
    using System.Linq;
    using System.Data.Common;
    using Configuration.Context;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;

    public class ExchangeRepository: IExchangeRepository
    {
        private readonly MoneyExchangeContext _context;

        public ExchangeRepository(MoneyExchangeContext context)
        {
            _context = context;
        }

        public IEnumerable<ExchangeType> GetExchangeTypes()
        {
            return _context.ExchangeType.ToList();
        }

        public ExchangeType GetTypeChangedAmount(string originCurrency, string destinationCurrency)
        {
            return _context.ExchangeType.SingleOrDefault(x => x.MonedaOrigen == originCurrency 
                                                     && x.MonedaDestino == destinationCurrency);
        }

        public void UpdateExchangeType(ExchangeType exchangeType, IDbTransaction transaction)
        {
            if (_context.Database.CurrentTransaction == null)
            {
                _context.Database.UseTransaction((DbTransaction)transaction);
            }

            _context.ExchangeType.Update(exchangeType);
            _context.SaveChanges();
        }
    }
}
