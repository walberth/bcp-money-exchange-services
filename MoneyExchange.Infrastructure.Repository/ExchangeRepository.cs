namespace MoneyExchange.Infrastructure.Repository
{
    using Entity;
    using Interfaces;
    using System.Data;
    using System.Linq;
    using System.Data.Common;
    using Configuration.Context;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;

    public class ExchangeRepository: IExchangeRepository
    {
        private readonly MoneyExchangeContext _context;

        public ExchangeRepository(MoneyExchangeContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ExchangeType>> GetExchangeTypes()
        {
            return await _context.ExchangeType.ToListAsync();
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

        public void RegisterMoneyExchangeType(ExchangeType exchangeType, IDbTransaction transaction)
        {
            if (_context.Database.CurrentTransaction == null)
            {
                _context.Database.UseTransaction((DbTransaction)transaction);
            }

            _context.ExchangeType.Add(exchangeType);
            _context.SaveChanges();
        }

        public void DeleteMoneyExchangeType(ExchangeType exchangeType, IDbTransaction transaction)
        {
            if (_context.Database.CurrentTransaction == null)
            {
                _context.Database.UseTransaction((DbTransaction)transaction);
            }

            _context.ExchangeType.Remove(exchangeType);
            _context.SaveChanges();
        }
    }
}
