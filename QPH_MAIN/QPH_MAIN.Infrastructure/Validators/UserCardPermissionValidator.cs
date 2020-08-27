using FluentValidation;
using QPH_MAIN.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace QPH_MAIN.Infrastructure.Validators
{
    public class UserCardPermissionValidator : AbstractValidator<UserCardPermissionDto>
    {
        public UserCardPermissionValidator()
        {
            RuleFor(city => city.Id_card_granted)
                .NotNull()
                .WithMessage("La tarjeta no puede ser nulo");

            RuleFor(city => city.Id_Permission)
                .NotNull()
                .WithMessage("El permiso no puede ser nulo");
        }
    }
}
