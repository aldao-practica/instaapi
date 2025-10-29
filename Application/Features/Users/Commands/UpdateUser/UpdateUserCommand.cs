using Application.Common;
using Application.DTOs.Users;
using MediatR;

namespace Application.Features.Users.Commands.UpdateUser;

public record UpdateUserCommand(
    Guid UserId,
    string? Bio,
    string? ProfilePictureUrl,
    bool? IsPrivate
) : IRequest<Result<UserDTO>>;
