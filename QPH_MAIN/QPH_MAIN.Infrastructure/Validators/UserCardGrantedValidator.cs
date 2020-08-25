using FluentValidation;
using QPH_MAIN.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace QPH_MAIN.Infrastructure.Validators
{
    public class UserCardGrantedValidator : AbstractValidator<UserCardGrantedDto>
    {
        public UserCardGrantedValidator()
        {
            RuleFor(city => city.Id_card)
                .NotNull()
                .WithMessage("La tarjeta no puede ser nulo");

            RuleFor(city => city.Id_user)
                .NotNull()
                .WithMessage("El usuario no puede ser nulo");
        }
    }
}
