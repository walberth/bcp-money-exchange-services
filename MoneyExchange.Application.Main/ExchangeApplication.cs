using MoneyExchange.Infrastructure.Entity;

namespace MoneyExchange.Application.Main
{
    using DTO;
    using System;
    using AutoMapper;
    using Interfaces;
    using System.Linq;
    using Transversal.Common;
    using Transversal.Validator;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using MoneyExchange.Infrastructure.Interfaces;

    public class ExchangeApplication: IExchangeApplication
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IExchangeRepository _exchangeRepository;

        ///<Summary>
        /// Constructor for Course
        ///</Summary>
        public ExchangeApplication(IExchangeRepository exchangeRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _exchangeRepository = exchangeRepository;
        }

        public async Task<Response<IEnumerable<ExchangeTypeDto>>> GetExchangeTypes()
        {
            var response = new Response<IEnumerable<ExchangeTypeDto>>();

            var exchangeTypes = await _exchangeRepository.GetExchangeTypes();

            if (!exchangeTypes.Any())
            {
                response.Message = Message.DidNotFindAnyResults;

                return response;
            }

            response.Data = _mapper.Map<IEnumerable<ExchangeTypeDto>>(exchangeTypes);
            response.IsWarning = false;

            return response;
        }

        public Response<ReturnExchangeDto> PerformMoneyExchange(ReceiveExchangeDto receiveExchange)
        {
            var response = new Response<ReturnExchangeDto>();

            var validator = new ReceiveExchangeValidator().Validate(receiveExchange);

            if (!validator.IsValid)
            {
                response.Message = validator.Errors.GetErrorMessage();

                return response;
            }

            var exchangeType = _exchangeRepository.GetTypeChangedAmount(receiveExchange.MonedaOrigen, receiveExchange.MonedaDestino);

            if (exchangeType == null)
            {
                response.Message = Message.DidNotFindTypeExchange;

                return response;
            }

            var returnExchange = _mapper.Map<ReturnExchangeDto>(exchangeType);
            returnExchange.MontoCambiado = Math.Round(receiveExchange.Monto * exchangeType.TipoCambio, 4);

            response.Data = returnExchange;
            response.IsWarning = false;

            return response;
        }

        public Response<object> ChangeMoneyExchangeType(ExchangeTypeDto exchangeType)
        {
            var response = new Response<object>();

            var validator = new ExchangeTypeValidator().Validate(exchangeType);

            if (!validator.IsValid)
            {
                response.Message = validator.Errors.GetErrorMessage();

                return response;
            }

            var exchangeTypeEntity = _exchangeRepository.GetTypeChangedAmount(exchangeType.MonedaOrigen, exchangeType.MonedaDestino);

            if (exchangeTypeEntity == null)
            {
                response.Message = Message.DidNotFindTypeExchange;

                return response;
            }

            using (var transaction = _unitOfWork?.BeginTransaction())
            {
                try
                {
                    exchangeTypeEntity.TipoCambio = exchangeType.TipoCambio;
                    _exchangeRepository.UpdateExchangeType(exchangeTypeEntity, transaction);

                    transaction?.Commit();
                }
                catch (Exception)
                {
                    transaction?.Rollback();
                    throw;
                }
            }

            response.IsWarning = false; 

            return response;
        }

        public Response<object> RegisterMoneyExchangeType(ExchangeTypeDto exchangeType)
        {
            var response = new Response<object>();

            var validator = new ExchangeTypeValidator().Validate(exchangeType);

            if (!validator.IsValid)
            {
                response.Message = validator.Errors.GetErrorMessage();

                return response;
            }

            using (var transaction = _unitOfWork?.BeginTransaction())
            {
                try
                {
                    _exchangeRepository.RegisterMoneyExchangeType(_mapper.Map<ExchangeType>(exchangeType), transaction);

                    transaction?.Commit();
                }
                catch (Exception)
                {
                    transaction?.Rollback();
                    throw;
                }
            }

            response.IsWarning = false;

            return response;
        }

        public Response<object> DeleteMoneyExchangeType(string originCurrency, string destinationCurrency)
        {
            var response = new Response<object>();

            var exchangeType = _exchangeRepository.GetTypeChangedAmount(originCurrency, destinationCurrency);

            if (exchangeType == null)
            {
                response.Message = Message.DidNotFindTypeExchange;

                return response;
            }

            using (var transaction = _unitOfWork?.BeginTransaction())
            {
                try
                {
                    _exchangeRepository.DeleteMoneyExchangeType(exchangeType, transaction);

                    transaction?.Commit();
                }
                catch (Exception)
                {
                    transaction?.Rollback();
                    throw;
                }
            }

            response.IsWarning = false;

            return response;
        }
    }
}
