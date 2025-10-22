using Application.Common;
using MediatR;

namespace Application.Features.Users.Commands.DeleteUser
{
    public record DeleteUserCommand
    (
        Guid UserId
    ) : IRequest<Result>;
}
