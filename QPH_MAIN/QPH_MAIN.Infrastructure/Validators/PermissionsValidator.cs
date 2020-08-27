using FluentValidation;
using QPH_MAIN.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace QPH_MAIN.Infrastructure.Validators
{
    public class PermissionsValidator : AbstractValidator<PermissionsDto>
    {
        public PermissionsValidator()
        {
            RuleFor(permission => permission.Permission)
                .NotNull()
                .WithMessage("El nombre del permiso no puede ser nulo");


            RuleFor(permission => permission.Permission)
                .Length(1, 10)
                .WithMessage("La longitud del permiso debe estar entre 1 y 10 caracteres");
        }
    }
}
