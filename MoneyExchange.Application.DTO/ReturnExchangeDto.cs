namespace MoneyExchange.Application.DTO
{
    public class ReturnExchangeDto
    {
        public double MontoCambiado { get; set; }
        public string MonedaOrigen { get; set; }
        public string MonedaDestino { get; set; }
        public double TipoCambio { get; set; }
    }
}
