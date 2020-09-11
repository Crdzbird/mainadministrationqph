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
            .WithMessage("Nombres de usuario no puede ser nulo.");
                    RuleFor(user => user.lastName)
            .NotNull()
            .WithMessage("Apellidos de usuario no puede ser nulo.");

            RuleFor(user => user.nickname)
                .NotNull()
                .WithMessage("El nickname de usuario no puede ser nulo.");

            RuleFor(user => user.email)
                .EmailAddress().WithMessage("Email no válido.")
                .NotNull()
                .WithMessage("El email de usuario no puede ser nulo.");

            RuleFor(user => user.hashPassword)
                .NotNull()
                .WithMessage("El password de usuario no puede ser nulo.");
        }
    }
}
