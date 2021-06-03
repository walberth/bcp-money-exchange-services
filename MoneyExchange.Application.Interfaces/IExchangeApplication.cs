using MoneyExchange.Transversal.Common;

namespace MoneyExchange.Application.Interfaces
{
    using DTO;
    using System.Collections.Generic;

    public interface IExchangeApplication
    {
        Response<IEnumerable<ExchangeTypeDto>> GetExchangeTypes();
    }
}
