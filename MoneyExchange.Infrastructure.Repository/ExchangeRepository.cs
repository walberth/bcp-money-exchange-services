namespace MoneyExchange.Infrastructure.Repository
{
    using Entity;
    using Interfaces;
    using System.Linq;
    using Configuration.Context;
    using System.Collections.Generic;

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
    }
}
