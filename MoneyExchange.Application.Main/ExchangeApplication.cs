namespace MoneyExchange.Application.Main
{
    using DTO;
    using AutoMapper;
    using Interfaces;
    using System.Linq;
    using Transversal.Common;
    using Infrastructure.Entity;
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
                response.Message = Message.NoSeEncontraronResultados;

                return response;
            }

            response.Data = _mapper.Map<IEnumerable<ExchangeTypeDto>>(exchangeTypes);
            response.IsWarning = false;

            return response;
        }
    }
}
