namespace MoneyExchange.Transversal.Validator
{
    using Application.DTO;
    using FluentValidation;
    using static FluentValidation.CascadeMode;

    public class ExchangeTypeValidator : AbstractValidator<ExchangeTypeDto>
    {
        public ExchangeTypeValidator()
        {
            RuleFor(x => x.TipoCambio)
                .Cascade(StopOnFirstFailure)
                .NotNull()
                .NotEmpty()
                .WithMessage("Debe indicar el nuevo monto del tipo de cambio");

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
