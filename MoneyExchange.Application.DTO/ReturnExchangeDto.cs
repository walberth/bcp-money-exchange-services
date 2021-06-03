namespace MoneyExchange.Application.DTO
{
    public class ReturnExchangeDto
    {
        public decimal MontoCambiado { get; set; }
        public string MonedaOrigen { get; set; }
        public string MonedaDestino { get; set; }
        public decimal TipoCambio { get; set; }
    }
}
