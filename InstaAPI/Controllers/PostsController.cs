using Application.DTOs.Posts;
using Application.Features.Posts.Commands.CreatePost;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InstaAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController : Controller
{
    private readonly IMediator _mediator;

    public PostsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] CreatePostDTO dto)
    {
        // TODO: Obtener el UserId del usuario autenticado (JWT)
        // Por ahora lo pasamos en el body o hardcodeamos para testing
        var userId = Guid.Parse(dto.UserId);

        var command = new CreatePostCommand(userId, dto.Content);
        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
            return BadRequest(new { error = result.Error });
        return CreatedAtAction(nameof(CreatePost), new { id = result.Data!.id }, result.Data);
    }
}
