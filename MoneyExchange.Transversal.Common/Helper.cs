namespace MoneyExchange.Transversal.Common
{
    using FluentValidation.Results;
    using System.Collections.Generic;

    public static class Helper
    {
        public static string GetErrorMessage(this IList<ValidationFailure> errors)
        {
            return string.Join(", ", errors);
        }
    }
}
