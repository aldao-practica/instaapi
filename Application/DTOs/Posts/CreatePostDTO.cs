namespace Application.DTOs.Posts;

public record CreatePostDTO(
    string UserId,
    string Content
);
