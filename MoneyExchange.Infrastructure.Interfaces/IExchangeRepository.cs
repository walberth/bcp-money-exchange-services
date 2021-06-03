namespace MoneyExchange.Infrastructure.Interfaces
{
    using Entity;
    using System.Collections.Generic;

    public interface IExchangeRepository
    {
        IEnumerable<ExchangeType> GetExchangeTypes();
    }
}
