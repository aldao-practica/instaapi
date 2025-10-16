namespace Application.DTOs.Users;

public record UpdateUserProfileDTO(
    string? Bio,
    string? ProfilePictureUrl
);
