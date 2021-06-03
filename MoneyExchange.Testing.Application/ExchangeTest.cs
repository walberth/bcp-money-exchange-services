namespace MoneyExchange.Testing.Application
{
    using Moq;
    using Data;
    using Xunit;
    using AutoMapper;
    using System.Data;
    using System.Linq;
    using Transversal.Common;
    using Infrastructure.Entity;
    using Infrastructure.Interfaces;
    using MoneyExchange.Application.DTO;
    using MoneyExchange.Application.Main;

    public class ExchangeTest
    {
        [Fact]
        public void GetExchangeTypes_ObtainAllExchangeTypes_ProcessedOk()
        {
            var mockExchangeRepository = new Mock<IExchangeRepository>();
            mockExchangeRepository.Setup(x => x.GetExchangeTypes())?.Returns(ExchangeData.GetExchangeTypesObtainOk());

            var mockDbTransaction = new Mock<IDbTransaction>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.BeginTransaction())
                ?.Returns(mockDbTransaction.Object);

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg?.CreateMap<ExchangeType, ExchangeTypeDto>()?.ReverseMap();
            }).CreateMapper();


            var response = new ExchangeApplication(mockExchangeRepository.Object, mockUnitOfWork.Object, mockMapper).GetExchangeTypes();

            Assert.True(response.Result.IsSuccess);
            Assert.False(response.Result.IsWarning);
            Assert.True(response.Result.Data.ToList().Any());
            Assert.True(response.Result.Data.ToList().Count == ExchangeData.GetExchangeTypesObtainOk().Result.Count());
        }

        [Fact]
        public void GetExchangeTypes_NotFoundAnyExchangeTypes_ProcessedOk()
        {
            var mockExchangeRepository = new Mock<IExchangeRepository>();
            mockExchangeRepository.Setup(x => x.GetExchangeTypes())?.Returns(ExchangeData.GetExchangeTypesNotFoundAny());

            var mockDbTransaction = new Mock<IDbTransaction>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.BeginTransaction())
                ?.Returns(mockDbTransaction.Object);

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg?.CreateMap<ExchangeType, ExchangeTypeDto>()?.ReverseMap();
            }).CreateMapper();


            var response = new ExchangeApplication(mockExchangeRepository.Object, mockUnitOfWork.Object, mockMapper).GetExchangeTypes();

            Assert.True(response.Result.IsSuccess);
            Assert.True(response.Result.IsWarning);
            Assert.True(response.Result.Message == Message.DidNotFindAnyResults);
        }

        [Fact]
        public void PerformMoneyExchange_NotSendAllParameters_ProcessedOk()
        {
            var receiveExchange = ExchangeData.GetReceiveExchangeIncomplete();
            var mockExchangeRepository = new Mock<IExchangeRepository>();
            mockExchangeRepository.Setup(x => x.GetExchangeTypes());

            var mockDbTransaction = new Mock<IDbTransaction>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.BeginTransaction())
                ?.Returns(mockDbTransaction.Object);

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg?.CreateMap<ExchangeType, ExchangeTypeDto>()?.ReverseMap();
            }).CreateMapper();


            var response = new ExchangeApplication(mockExchangeRepository.Object, mockUnitOfWork.Object, mockMapper).PerformMoneyExchange(receiveExchange);

            Assert.True(response.IsSuccess);
            Assert.True(response.IsWarning);
            Assert.True(response.Message.Contains("Debe indicar el monto que desea cambiar"));
        }

        [Fact]
        public void PerformMoneyExchange_SendAllParameters_ProcessedOk()
        {
            var receiveExchange = ExchangeData.GetReceiveExchangeComplete();
            var returnExchangeType = ExchangeData.GetExchangeType();
            var amountChanged = 26;
            var mockExchangeRepository = new Mock<IExchangeRepository>();
            mockExchangeRepository.Setup(x => x.GetTypeChangedAmount(receiveExchange.MonedaOrigen, receiveExchange.MonedaDestino))
                .Returns(returnExchangeType);

            var mockDbTransaction = new Mock<IDbTransaction>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(x => x.BeginTransaction())
                ?.Returns(mockDbTransaction.Object);

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg?.CreateMap<ExchangeType, ReturnExchangeDto>()?.ReverseMap();
            }).CreateMapper();


            var response = new ExchangeApplication(mockExchangeRepository.Object, mockUnitOfWork.Object, mockMapper).PerformMoneyExchange(receiveExchange);

            Assert.True(response.IsSuccess);
            Assert.False(response.IsWarning);
            Assert.True(response.Data.MontoCambiado == amountChanged);
        }
    }
}
