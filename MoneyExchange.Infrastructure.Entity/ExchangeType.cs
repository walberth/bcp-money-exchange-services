namespace MoneyExchange.Infrastructure.Entity
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("exchange_type")]
    public class ExchangeType
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("moneda_origen")]
        public string MonedaOrigen { get; set; }

        [Column("moneda_destino")]
        public string MonedaDestino { get; set; }

        [Column("tipo_cambio")]
        public double TipoCambio { get; set; }
    }
}
