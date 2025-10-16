namespace Application.DTOs.Users;
public record UserDTO(
    Guid Id,
    string Username,
    string Email,
    string? ProfilePictureUrl,
    string? Bio,
    bool IsPrivate,
    DateTime CreatedAt
);
