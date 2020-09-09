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

            RuleFor(enterprise => enterprise.commercial_name)
                .Length(1, 100)
                .WithMessage("La longitud del nombre comomercial debe estar entre 1 y 100 caracteres");

            RuleFor(e => e.name_application)
               .NotNull()
               .WithMessage("El nombre de aplicacion no puede ser nulo");

            RuleFor(enterprise => enterprise.name_application)
                .Length(1, 300)
                .WithMessage("La longitud del nombre de aplicacion debe estar entre 1 y 300");

            RuleFor(e => e.telephone)
               .NotNull()
               .WithMessage("El telefono no puede ser nulo");

            RuleFor(enterprise => enterprise.telephone)
                .Length(1, 20)
                .WithMessage("La longitud del telefono debe estar entre 1 y 20 caracteres");

            RuleFor(e => e.email)
               .NotNull()
               .EmailAddress()
               .WithMessage("El email no puede ser nulo");

            RuleFor(e => e.has_branches)
               .NotNull()
               .WithMessage("La ramificacion no puede ser nulo");

            RuleFor(e => e.enterprise_address)
               .NotNull()
               .WithMessage("La direccion no puede ser nulo");

            RuleFor(e => e.identification)
               .NotNull()
               .WithMessage("La identificacion de empresa no puede ser nulo");
        }
    }
}
