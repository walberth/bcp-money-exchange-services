namespace MoneyExchange.Infrastructure.Interfaces
{
    using Entity;
    using System.Data;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface IExchangeRepository
    {
        Task<IEnumerable<ExchangeType>> GetExchangeTypes();
        ExchangeType GetTypeChangedAmount(string originCurrency, string destinationCurrency);
        void UpdateExchangeType(ExchangeType exchangeType, IDbTransaction transaction);
        void RegisterMoneyExchangeType(ExchangeType exchangeType, IDbTransaction transaction);
        void DeleteMoneyExchangeType(ExchangeType exchangeType, IDbTransaction transaction);
    }
}
