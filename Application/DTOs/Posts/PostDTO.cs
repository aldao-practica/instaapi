namespace Application.DTOs.Posts;

public record PostDTO(
    Guid id,
    Guid UserId,
    string Content
);
