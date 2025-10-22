using Application.Common;
using Application.DTOs.Users;
using Application.Features.Users.Queries.GetUser;
using Domain.Interfaces;
using MediatR;
using System.Text.Json;

namespace Application.Features.Users.Queries.GetAllUsers;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, Result<UserDTO[]>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllUsersQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<UserDTO[]>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _unitOfWork.Users.GetAllAsync(cancellationToken);

        var userDTOs = users.Select(u => new UserDTO(
            u.Id,
            u.Username,
            u.Email,
            u.ProfilePictureUrl,
            u.Bio,
            u.IsPrivate,
            u.CreatedAt
        )).ToArray();

        return Result <UserDTO[]>.Success(userDTOs);
    }
}
