using FluentValidation;
using QPH_MAIN.Core.DTOs;

namespace QPH_MAIN.Infrastructure.Validators
{
    public class RegionValidator : AbstractValidator<RegionDto>
    {
        public RegionValidator()
        {
            RuleFor(region => region.Name)
                .NotNull()
                .WithMessage("El nombre de la region no puede ser nulo");

            RuleFor(region => region.Id_country)
                .NotNull()
                .WithMessage("El pais no puede ser nulo");
        }
    }
}
