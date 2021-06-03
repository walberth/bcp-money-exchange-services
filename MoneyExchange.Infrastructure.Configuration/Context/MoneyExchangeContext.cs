using System.Reflection;

namespace MoneyExchange.Infrastructure.Configuration.Context
{
    using Entity;
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
            optionsBuilder.UseSqlite("Filename=bcp.db", options =>
            {
                options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExchangeType>().HasData(
                new ExchangeType { Id = 1, MonedaOrigen = "PEN", MonedaDestino = "USD", TipoCambio = 3.7 },
                new ExchangeType { Id = 2, MonedaOrigen = "USD", MonedaDestino = "PEN", TipoCambio = 1.25 },
                new ExchangeType { Id = 3, MonedaOrigen = "EUR", MonedaDestino = "PEN", TipoCambio = 3.2 }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}
