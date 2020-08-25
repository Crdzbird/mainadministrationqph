using FluentValidation;
using QPH_MAIN.Core.DTOs;

namespace QPH_MAIN.Infrastructure.Validators
{
    public class CardsValidator : AbstractValidator<CardsDto>
    {
        public CardsValidator()
        {
            RuleFor(city => city.Card)
                .NotNull()
                .WithMessage("El nombre de la tarjeta no puede ser nulo");
        }
    }
}
