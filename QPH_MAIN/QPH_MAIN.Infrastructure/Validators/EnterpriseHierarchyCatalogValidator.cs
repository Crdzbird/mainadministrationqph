using FluentValidation;
using QPH_MAIN.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace QPH_MAIN.Infrastructure.Validators
{
    public class EnterpriseHierarchyCatalogValidator : AbstractValidator<EnterpriseHierarchyCatalogDto>
    {
        public EnterpriseHierarchyCatalogValidator()
        {
            RuleFor(ehc => ehc.EnterpriseId)
                .NotNull()
                .WithMessage("Se requiere una empresa para asociar al catalogo.");

            RuleFor(ehc => ehc.Parent)
                .NotNull()
                .WithMessage("El catalogo padre no puede ser nulo");

            RuleFor(ehc => ehc.Children)
                .NotNull()
                .WithMessage("El catalogo hijo no puede ser nulo");
        }
    }
}
