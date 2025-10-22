using FluentValidation;

namespace Application.Features.Users.Commands.CreateUser;

//¿Por qué FluentValidation?
//Más legible que Data Annotations
//Reutilizable y testeable
//Separado de los DTOs(Single Responsibility)

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username es requerido")
            .Length(3, 30).WithMessage("Username debe tener entre 3 y 30 caracteres")
            .Matches("^[a-zA-Z0-9._]+$").WithMessage("Username solo puede contener letras, números, puntos y guiones bajos");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email es requerido")
            .EmailAddress().WithMessage("Email inválido")
            .MaximumLength(100).WithMessage("Email muy largo");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password es requerido")
            .MinimumLength(8).WithMessage("Password debe tener al menos 8 caracteres")
            .Matches("[A-Z]").WithMessage("Password debe contener al menos una mayúscula")
            .Matches("[a-z]").WithMessage("Password debe contener al menos una minúscula")
            .Matches("[0-9]").WithMessage("Password debe contener al menos un número");
    }
}
