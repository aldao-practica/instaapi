using FluentValidation;

namespace Application.Features.Users.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId es requerido")
            .NotEqual(Guid.Empty).WithMessage("UserId inválido");

        RuleFor(x => x.Bio)
            .MaximumLength(500).WithMessage("Bio no puede superar los 500 caracteres")
            .When(x => x.Bio != null); // Solo validamos si el campo fue enviado.

        RuleFor(x => x.ProfilePictureUrl)
            .MaximumLength(500).WithMessage("ProfilePictureUrl muy largo")
            .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
            .WithMessage("ProfilePictureUrl debe ser una URL válida")
            .When(x => x.ProfilePictureUrl != null);
    }
}
