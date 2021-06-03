using MoneyExchange.Application.DTO;

namespace MoneyExchange.Testing.Application.Data
{
    using Infrastructure.Entity;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    public static class ExchangeData
    {
        public static async Task<IEnumerable<ExchangeType>> GetExchangeTypesObtainOk()
        {
            return await Task.Run(() => new List<ExchangeType> {
                new ExchangeType { Id = 1, MonedaOrigen = "PEN", MonedaDestino = "USD", TipoCambio = 0.26 },
                new ExchangeType { Id = 2, MonedaOrigen = "PEN", MonedaDestino = "EUR", TipoCambio = 0.21 },
                new ExchangeType { Id = 3, MonedaOrigen = "USD", MonedaDestino = "PEN", TipoCambio = 3.85 },
                new ExchangeType { Id = 4, MonedaOrigen = "USD", MonedaDestino = "EUR", TipoCambio = 0.82 },
                new ExchangeType { Id = 5, MonedaOrigen = "EUR", MonedaDestino = "PEN", TipoCambio = 4.70 },
                new ExchangeType { Id = 6, MonedaOrigen = "EUR", MonedaDestino = "USD", TipoCambio = 1.22 }
            });
        }

        public static async Task<IEnumerable<ExchangeType>> GetExchangeTypesNotFoundAny()
        {
            return await Task.Run(() => new List<ExchangeType>());
        }

        public static ReceiveExchangeDto GetReceiveExchangeIncomplete()
        {
            return new ReceiveExchangeDto()
            {
                Monto = 0,
                MonedaOrigen = "PEN",
                MonedaDestino = null
            };
        }

        public static ReceiveExchangeDto GetReceiveExchangeComplete()
        {
            return new ReceiveExchangeDto()
            {
                Monto = 100,
                MonedaOrigen = "PEN",
                MonedaDestino = "USD"
            };
        }

        public static ExchangeType GetExchangeType()
        {
            return new ExchangeType()
            {
                Id = 1,
                TipoCambio = 0.26,
                MonedaOrigen = "PEN",
                MonedaDestino = "USD"
            };
        }
    }
}
