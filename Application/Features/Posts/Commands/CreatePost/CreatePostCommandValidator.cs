using FluentValidation;

namespace Application.Features.Posts.Commands.CreatePost;

public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("UserId es requerido")
            .NotEqual(Guid.Empty).WithMessage("UserId inválido");

        RuleFor(x => x.Content)
            .MaximumLength(2200).WithMessage("Content no puede superar los 2200 caracteres");
    }
}
