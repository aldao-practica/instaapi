using Application.Common;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Users.Commands.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var exists = await _unitOfWork.Users.GetByIdAsync(request.UserId, cancellationToken);
        if (exists == null)
            return Result.Failure("El usuario no existe");
        await _unitOfWork.Users.DeleteAsync(exists, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }

}
