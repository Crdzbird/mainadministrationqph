using FluentValidation;
using QPH_MAIN.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace QPH_MAIN.Infrastructure.Validators
{
    public class UserViewValidator : AbstractValidator<UserViewDto>
    {
        public UserViewValidator()
        {
            RuleFor(view => view.UserId)
                .NotNull()
                .WithMessage("Se requiere un usuario para asociar la vista.");

            RuleFor(view => view.Parent)
                .NotNull()
                .WithMessage("La vista padre no puede ser nulo");

            RuleFor(view => view.Children)
                .NotNull()
                .WithMessage("La vista hija no puede ser nulo");
        }
    }
}
