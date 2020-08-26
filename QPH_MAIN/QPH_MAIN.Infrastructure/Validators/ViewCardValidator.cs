using FluentValidation;
using QPH_MAIN.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace QPH_MAIN.Infrastructure.Validators
{
    public class ViewCardValidator : AbstractValidator<ViewCardDto>
    {
        public ViewCardValidator()
        {
            RuleFor(viewCard => viewCard.Id_card)
                .NotNull()
                .WithMessage("La tarjeta no puede ser nulo");

            RuleFor(viewCard => viewCard.Id_view)
                .NotNull()
                .WithMessage("La vista no puede ser nulo");
        }
    }
}
