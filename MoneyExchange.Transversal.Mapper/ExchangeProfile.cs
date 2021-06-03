namespace MoneyExchange.Transversal.Mapper
{
    using Application.DTO;
    using Infrastructure.Entity;

    public class ExchangeProfile : AutoMapper.Profile
    {
        public ExchangeProfile()
        {
            CreateMap<ExchangeTypeDto, ExchangeType>()?.ReverseMap();
            CreateMap<ReturnExchangeDto, ExchangeType>()?.ReverseMap();
            CreateMap<ReceiveExchangeDto, ExchangeType>()?.ReverseMap();
            CreateMap<ReceiveExchangeDto, ReturnExchangeDto>()?.ReverseMap();
            //?.ForMember(dest => dest.MonedaOrigen, opt => opt?.MapFrom(src => src.MonedaOrigen))
            //?.ForMember(dest => dest.MonedaDestino, opt => opt?.MapFrom(src => src.MonedaDestino))
            //?.ForMember(dest => dest.TipoCambio, opt => opt?.MapFrom(src => src.TipoCambio));
        }
    }
}
