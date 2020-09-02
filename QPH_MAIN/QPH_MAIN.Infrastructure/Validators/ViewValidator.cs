using FluentValidation;
using QPH_MAIN.Core.DTOs;
using QPH_MAIN.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QPH_MAIN.Infrastructure.Validators
{
    public class ViewValidator : AbstractValidator<ViewsDto>
    {
        public ViewValidator()
        {
            RuleFor(view => view.Name)
                .NotNull()
                .WithMessage("El nombre de la vista no puede ser nulo");

            RuleFor(view => view.Name)
                .Length(1, 50)
                .WithMessage("La longitud del nombre de la vista debe estar entre 1 y 50 caracteres");

            RuleFor(view => view.Code)
                .NotNull()
                .WithMessage("El codigo de la vista no puede ser nulo");

            RuleFor(view => view.Route)
                .NotNull()
                .WithMessage("La ruta de la vista no puede ser nulo");

            /*RuleFor(view => view.Code)
                .Must(UniqueCode)
                .WithMessage("El codigo ya existe");*/

            RuleFor(view => view.Code)
                .Length(1, 10)
                .WithMessage("La longitud del codigo de vista debe estar entre 1 y 10 caracteres");
        }

        private bool UniqueCode(ViewsDto viewsDto, string name)
        {
            QPHContext _db = new QPHContext();
            var dbView = _db.Views.Where(x => x.code.ToLower() == name.ToLower()).SingleOrDefault();
            if (dbView == null)
                return true;
            return dbView.Id == viewsDto.Id;
        }
    }
}
