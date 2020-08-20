using FluentValidation;
using QPH_MAIN.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace QPH_MAIN.Infrastructure.Validators
{
    public class ViewValidator : AbstractValidator<ViewsDto>
    {
        public ViewValidator()
        {
            RuleFor(view => view.Name)
                .NotNull()
                .WithMessage("El nombre de la vista no puede ser nulo");

            RuleFor(view => view.Code)
                .NotNull()
                .WithMessage("El codigo de la vista no puede ser nulo");
        }
    }
}
