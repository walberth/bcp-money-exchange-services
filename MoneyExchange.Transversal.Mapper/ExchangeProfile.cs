namespace MoneyExchange.Transversal.Mapper
{
    using Application.DTO;
    using Infrastructure.Entity;

    public class ExchangeProfile : AutoMapper.Profile
    {
        public ExchangeProfile()
        {
            CreateMap<ExchangeTypeDto, ExchangeType>()?.ReverseMap();
        }
    }
}
