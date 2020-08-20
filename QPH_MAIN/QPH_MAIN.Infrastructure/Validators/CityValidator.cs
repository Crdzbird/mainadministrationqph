using FluentValidation;
using QPH_MAIN.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace QPH_MAIN.Infrastructure.Validators
{
    public class CityValidator : AbstractValidator<CityDto>
    {
        public CityValidator()
        {
            RuleFor(city => city.Name)
                .NotNull()
                .WithMessage("El nombre de la ciudad no puede ser nulo");

            RuleFor(city => city.id_region)
                .NotNull()
                .WithMessage("La region no puede ser nulo");
        }
    }
}
