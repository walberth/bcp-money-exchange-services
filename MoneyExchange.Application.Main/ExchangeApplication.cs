namespace MoneyExchange.Application.Main
{
    using DTO;
    using System;
    using AutoMapper;
    using Interfaces;
    using System.Linq;
    using Transversal.Common;
    using Infrastructure.Entity;
    using Transversal.Validator;
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

        public Response<IEnumerable<ExchangeTypeDto>> GetExchangeTypes()
        {
            var response = new Response<IEnumerable<ExchangeTypeDto>>();

            var exchangeTypes = _exchangeRepository.GetExchangeTypes().ToList();

            if (!exchangeTypes.Any())
            {
                response.Message = Message.DidNotFindAnyResults;

                return response;
            }

            response.Data = _mapper.Map<IEnumerable<ExchangeTypeDto>>(exchangeTypes);
            response.IsWarning = false;

            return response;
        }

        public Response<ReturnExchangeDto> RealizeMoneyExchange(ReceiveExchangeDto receiveExchange)
        {
            var response = new Response<ReturnExchangeDto>();

            var validator = new ReceiveExchangeValidator().Validate(receiveExchange);

            if (!validator.IsValid)
            {
                response.Message = validator.Errors.GetErrorMessage();

                return response;
            }

            var exchangeType = _exchangeRepository.GetTypeChangedAmount(receiveExchange.MonedaOrigen, 
                receiveExchange.MonedaDestino);

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
    }
}
