using FluentValidation;

namespace Application.Features.Users.Commands.DeleteUser;

public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId es requerido");
        RuleFor(x => x.UserId)
            .Must(id => id != Guid.Empty).WithMessage("UserId inválido");
    }
}
