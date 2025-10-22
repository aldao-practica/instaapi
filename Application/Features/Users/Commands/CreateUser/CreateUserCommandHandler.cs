using Application.Common;
using Application.DTOs.Users;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Users.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<UserDTO>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<UserDTO>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var exists = await _unitOfWork.Users.ExistsAsync(request.Email, request.Username, cancellationToken);
        if (exists)
            return Result<UserDTO>.Failure("El email o username ya existe");

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var user = User.Create(request.Username, request.Email, passwordHash);

        await _unitOfWork.Users.AddAsync(user, cancellationToken);
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
