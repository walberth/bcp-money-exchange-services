namespace MoneyExchange.Transversal.Validator
{
    using Application.DTO;
    using FluentValidation;
    using static FluentValidation.CascadeMode;

    public class ReceiveExchangeValidator : AbstractValidator<ReceiveExchangeDto>
    {
        public ReceiveExchangeValidator()
        {
            RuleFor(x => x.Monto)
                .Cascade(StopOnFirstFailure)
                .NotNull()
                .NotEmpty()
                .WithMessage("Debe indicar el monto que desea cambiar");

            RuleFor(x => x.MonedaOrigen)
                .Cascade(StopOnFirstFailure)
                .NotNull()
                .NotEmpty()
                .WithMessage("Debe indicar la moneda de origen para realizar el cambio");

            RuleFor(x => x.MonedaDestino)
                .Cascade(StopOnFirstFailure)
                .NotNull()
                .NotEmpty()
                .WithMessage("Debe indicar la moneda de destino para realizar el cambio");
        }
    }
}
