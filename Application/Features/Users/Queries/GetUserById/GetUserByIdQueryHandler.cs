using Application.Common;
using Application.DTOs.Users;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserDTO>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserByIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<UserDTO>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(request.UserId, cancellationToken);

        if (user == null)
            return Result<UserDTO>.Failure("Usuario no encontrado");

        var userDto = new UserDTO(
            user.Id,
            user.Username,
            user.Email,
            user.ProfilePictureUrl,
        user.Bio,
            user.IsPrivate,
            user.CreatedAt
        );

        return Result<UserDTO>.Success(userDto);
    }
}
