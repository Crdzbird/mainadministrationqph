
using FluentValidation;
using QPH_MAIN.Core.DTOs;

namespace QPH_MAIN.Infrastructure.Validators
{
    public class RolesValidator : AbstractValidator<RolesDto>
    {
        public RolesValidator()
        {

            RuleFor(roles => roles.rolename)
                .NotNull()
                .WithMessage("El nombre de rol de usuario no puede ser nulo");

            RuleFor(roles => roles.status)
                .NotNull()
                .WithMessage("El status del rol no puede ser nulo");

        }
    }
}
