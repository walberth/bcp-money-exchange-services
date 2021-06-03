namespace MoneyExchange
{
    using Service.Api.Core;
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Infrastructure.Configuration.Context;

    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateHostBuilder(args).Build().CreateSqlite<MoneyExchangeContext>().Run();
            CreateWebHostBuilder(args).Build().CreateSqlite<MoneyExchangeContext>().Run();
        }

        /*public static /*IHostBuilderIWebHostBuilder CreateHostBuilder(string[] args) =>
                /*Host.CreateDefaultBuilder(args)
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseStartup<Startup>();
                    });WebHost.CreateDefaultBuilder(args)
                    .UseUrls(Environment.GetEnvironmentVariable("URL_PATH"))
                    .UseStartup<Startup>();*/
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
