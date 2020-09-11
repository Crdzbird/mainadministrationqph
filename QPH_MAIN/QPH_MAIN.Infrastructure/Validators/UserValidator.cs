using FluentValidation;
using QPH_MAIN.Core.DTOs;

namespace QPH_MAIN.Infrastructure.Validators
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator()
        {

            RuleFor(user => user.firstName)
                .NotNull()
                .WithMessage("El primer nombre de usuario no puede ser nulo");

            RuleFor(user => user.lastName)
                .NotNull()
                .WithMessage("El apellido de usuario no puede ser nulo");

            RuleFor(user => user.nickname)
                .NotNull()
                .WithMessage("El nickname de usuario no puede ser nulo");

            RuleFor(user => user.email)
                .EmailAddress()
                .NotNull()
                .WithMessage("El email de usuario no puede ser nulo");

            RuleFor(user => user.hashPassword)
                .NotNull()
                .WithMessage("El password de usuario no puede ser nulo");
        }
    }
}
