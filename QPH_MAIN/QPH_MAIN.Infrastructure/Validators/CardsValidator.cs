using FluentValidation;
using QPH_MAIN.Core.DTOs;

namespace QPH_MAIN.Infrastructure.Validators
{
    public class CardsValidator : AbstractValidator<CardsDto>
    {
        public CardsValidator()
        {
            RuleFor(card => card.Card)
                .NotNull()
                .WithMessage("El nombre de la tarjeta no puede ser nulo");

            RuleFor(card => card.Card)
                .Length(1, 40)
                .WithMessage("La longitud de la tarjeta debe estar entre 1 y 40 caracteres");
        }
    }
}
