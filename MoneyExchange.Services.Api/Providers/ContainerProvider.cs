namespace MoneyExchange.Service.Api.Providers
{
    using AutoMapper;
    using Application.Main;
    using Transversal.Common;
    using Transversal.Mapper;
    using Application.Interfaces;
    using Infrastructure.Interfaces;
    using Infrastructure.Repository;
    using Infrastructure.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    ///<Summary>
    /// Provider for dependency injection of classes
    ///</Summary>
    public static class ContainerProvider
    {
        ///<Summary>
        /// Constructor for provider
        ///</Summary>
        public static IServiceCollection ConfigureServiceCollection(this IServiceCollection services)
        {
            ConfigureContainer(services);
            ConfigureMapper(services);

            return services;
        }

        static void ConfigureContainer(IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IExchangeApplication, ExchangeApplication>();
            services.AddTransient<IExchangeRepository, ExchangeRepository>();
        }

        static void ConfigureMapper(IServiceCollection services)
        {
            var automapperConfig = new MapperConfiguration(configuration => {
                configuration.AddProfile(new ExchangeProfile());
            });

            services.AddSingleton(automapperConfig.CreateMapper());
        }
    }
}
