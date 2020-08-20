using FluentValidation;
using QPH_MAIN.Core.DTOs;

namespace QPH_MAIN.Infrastructure.Validators
{
    public class EnterpriseValidator : AbstractValidator<EnterpriseDto>
    {
        public EnterpriseValidator()
        {
            RuleFor(e => e.commercial_name)
               .NotNull()
               .WithMessage("El nombre comercial no puede ser nulo");

            RuleFor(e => e.telephone)
               .NotNull()
               .WithMessage("El telefono no puede ser nulo");

            RuleFor(e => e.email)
               .NotNull()
               .WithMessage("El email no puede ser nulo");

            RuleFor(e => e.telephone)
               .NotNull()
               .WithMessage("El telefono no puede ser nulo");

            RuleFor(e => e.enterprise_address)
               .NotNull()
               .WithMessage("La direccion no puede ser nulo");

            RuleFor(e => e.identification)
               .NotNull()
               .WithMessage("La identificacion de empresa no puede ser nulo");

            RuleFor(e => e.telephone)
               .NotNull()
               .WithMessage("El telefono no puede ser nulo");
        }
    }
}
