using FluentValidation;
using QPH_MAIN.Core.DTOs;

namespace QPH_MAIN.Infrastructure.Validators
{
    public class CountryValidator : AbstractValidator<CountryDto>
    {
        public CountryValidator()
        {
            RuleFor(country => country.Name)
                .NotNull()
                .WithMessage("El nombre del pais no puede ser nulo");
        }
    }
}
