using System.Data;
using System.Linq;
using MoneyExchange.Application.Main;
using MoneyExchange.Transversal.Common;

namespace MoneyExchange.Testing.Application
{
    using Moq;
    using Data;
    using Xunit;
    using System;
    using AutoMapper;
    using Infrastructure.Entity;
    using Infrastructure.Interfaces;
    using MoneyExchange.Application.DTO;


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
    }
}
