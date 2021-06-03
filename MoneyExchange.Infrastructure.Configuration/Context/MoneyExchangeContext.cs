namespace MoneyExchange.Infrastructure.Configuration.Context
{
    using Entity;
    using System.Reflection;
    using Microsoft.EntityFrameworkCore;

    public sealed class MoneyExchangeContext : DbContext
    {
        public MoneyExchangeContext() { }

        public MoneyExchangeContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
            ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<ExchangeType> ExchangeType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=Sqlite/bcp.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExchangeType>().HasData(
                new ExchangeType { Id = 1, MonedaOrigen = "PEN", MonedaDestino = "USD", TipoCambio = 0.26 },
                new ExchangeType { Id = 2, MonedaOrigen = "PEN", MonedaDestino = "EUR", TipoCambio = 0.21 },
                new ExchangeType { Id = 3, MonedaOrigen = "USD", MonedaDestino = "PEN", TipoCambio = 3.85 },
                new ExchangeType { Id = 4, MonedaOrigen = "USD", MonedaDestino = "EUR", TipoCambio = 0.82 },
                new ExchangeType { Id = 5, MonedaOrigen = "EUR", MonedaDestino = "PEN", TipoCambio = 4.70 },
                new ExchangeType { Id = 6, MonedaOrigen = "EUR", MonedaDestino = "USD", TipoCambio = 1.22 }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}
