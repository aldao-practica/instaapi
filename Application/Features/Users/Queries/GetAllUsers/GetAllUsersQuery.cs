using Application.Common;
using Application.DTOs.Users;
using MediatR;

namespace Application.Features.Users.Queries.GetUser;

public record GetAllUsersQuery() : IRequest<Result<UserDTO[]>>;
