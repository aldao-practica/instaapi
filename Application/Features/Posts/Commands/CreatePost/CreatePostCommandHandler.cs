using Application.Common;
using Application.DTOs.Posts;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Posts.Commands.CreatePost;

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, Result<PostDTO>>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreatePostCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<PostDTO>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var post = Post.Create(request.UserId, request.Content);

        await _unitOfWork.Posts.AddAsync(post, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var postDTO = new PostDTO
        (
            post.Id,
            post.UserId,
            post.Content
        );

        return Result<PostDTO>.Success(postDTO);
    }
}
