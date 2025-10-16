using Application.Common;
using Application.DTOs.Users;
using MediatR;
using System.Text.RegularExpressions;

namespace Application.Features.Users.Commands.CreateUser;

public record CreateUserCommand(
    string Username,
    string Email,
    string Password
) : IRequest<Result<UserDTO>>;

//IRequest viene de MediatR - marca esto como un "comando" que se puede enviar
//Result<UserDto> es lo que devuelve cuando se ejecuta
//MediatR se encarga de encontrar quién maneja este comando
