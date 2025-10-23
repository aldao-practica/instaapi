using Application.Common;
using Application.DTOs.Posts;
using MediatR;

namespace Application.Features.Posts.Commands.CreatePost;

public record CreatePostCommand(
    Guid UserId,
    string Content
) : IRequest<Result<PostDTO>>;
