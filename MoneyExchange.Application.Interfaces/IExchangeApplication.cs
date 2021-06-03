namespace MoneyExchange.Application.Interfaces
{
    using DTO;
    using Transversal.Common;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public interface IExchangeApplication
    {
        Task<Response<IEnumerable<ExchangeTypeDto>>> GetExchangeTypes(); 
        Response<ReturnExchangeDto> PerformMoneyExchange(ReceiveExchangeDto receiveExchange);
        Response<object> ChangeMoneyExchangeType(ExchangeTypeDto exchangeType);
        Response<object> RegisterMoneyExchangeType(ExchangeTypeDto exchangeType);
        Response<object> DeleteMoneyExchangeType(string originCurrency, string destinationCurrency);
    }
}
