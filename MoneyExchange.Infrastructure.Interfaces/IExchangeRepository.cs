namespace MoneyExchange.Infrastructure.Interfaces
{
    using Entity;
    using System.Data;
    using System.Collections.Generic;

    public interface IExchangeRepository
    {
        IEnumerable<ExchangeType> GetExchangeTypes();
        ExchangeType GetTypeChangedAmount(string originCurrency, string destinationCurrency);
        void UpdateExchangeType(ExchangeType exchangeType, IDbTransaction transaction);
    }
}
