using Application.Common;
using Application.DTOs.Users;
using MediatR;
namespace Application.Features.Users.Queries.GetUserById;

public record GetUserByIdQuery(Guid UserId) : IRequest<Result<UserDTO>>;