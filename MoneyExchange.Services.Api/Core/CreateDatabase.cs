namespace MoneyExchange.Service.Api.Core
{
    using System;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.DependencyInjection;

    public static class CreateDatabase
    {
        public static IWebHost CreateSqlite<T>(this IWebHost webHost) where T : DbContext
        {
            using var scope = webHost.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var db = services.GetRequiredService<T>();
                db.Database.Migrate();
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "Database Creation/Migrations failed!");
            }

            return webHost;
        }
    }
}
