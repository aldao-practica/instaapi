using Application.Common;
using Application.DTOs.Users;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<UserDTO>>
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<UserDTO>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(request.UserId, cancellationToken);

        if (user == null)
            return Result<UserDTO>.Failure("Usuario no encontrado");

        // Solo actualizar los campos que se enviaron (no null)
        if (request.Bio != null || request.ProfilePictureUrl != null)
        {
            user.UpdateProfile(request.Bio, request.ProfilePictureUrl);
        }

        if (request.IsPrivate.HasValue)
        {
            user.SetPrivacy(request.IsPrivate.Value);
        }

        await _unitOfWork.Users.UpdateAsync(user, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

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
