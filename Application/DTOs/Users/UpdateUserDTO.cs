namespace Application.DTOs.Users;

public record UpdateUserDTO(
    string? Bio,
    string? ProfilePictureUrl,
    bool? IsPrivate
);
