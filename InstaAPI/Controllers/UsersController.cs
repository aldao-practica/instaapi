using Application.DTOs.Users;
using Application.Features.Users.Commands.CreateUser;
using Application.Features.Users.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InstaAPI.Controllers;

//¿Qué hace el Controller?

//Es DELGADO: Solo recibe requests y delega a MediatR
//No tiene lógica de negocio: Todo está en los Handlers
//Usa IMediator: Envía Commands y Queries
//Devuelve códigos HTTP apropiados: 201 Created, 200 OK, 404 NotFound, 400 BadRequest

//¿Por qué IMediator?

//Desacopla el Controller de los Handlers
//El Controller no sabe quién maneja el comando, solo lo envía
//Facilita testing(mockear IMediator es fácil)

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO dto)
    {
        var command = new CreateUserCommand(dto.Username, dto.Email, dto.Password);
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error });

        return CreatedAtAction(nameof(GetUserById), new { id = result.Data!.Id }, result.Data);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var query = new GetUserByIdQuery(id);
        var result = await _mediator.Send(query);

        if (!result.IsSuccess)
            return NotFound(new { error = result.Error });

        return Ok(result.Data);
    }
}
