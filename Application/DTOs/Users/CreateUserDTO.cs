namespace Application.DTOs.Users;
public record CreateUserDTO(
    string Username,
    string Email,
    string Password
);
