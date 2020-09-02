using FluentValidation;
using QPH_MAIN.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace QPH_MAIN.Infrastructure.Validators
{
    public class SystemParametersValidator : AbstractValidator<SystemParametersDto>
    {
        public SystemParametersValidator()
        {
            RuleFor(systemParameters => systemParameters.Code)
                .NotNull()
                .WithMessage("El codigo no puede ser nulo");

            RuleFor(systemParameters => systemParameters.Description)
                .NotNull()
                .WithMessage("La descripcion no puede ser nulo");

            RuleFor(systemParameters => systemParameters.Value)
                .NotNull()
                .WithMessage("El valor no puede ser nulo");

            RuleFor(systemParameters => systemParameters.DataType)
                .NotNull()
                .WithMessage("El tipo de dato no puede ser nulo");

            RuleFor(systemParameters => systemParameters.Status)
                .NotNull()
                .WithMessage("El estado no puede ser nulo");
        }
    }
}
