namespace MoneyExchange.Application.Interfaces
{
    using DTO;
    using Transversal.Common;
    using System.Collections.Generic;

    public interface IExchangeApplication
    {
        Response<IEnumerable<ExchangeTypeDto>> GetExchangeTypes(); 
        Response<ReturnExchangeDto> RealizeMoneyExchange(ReceiveExchangeDto receiveExchange);
    }
}
