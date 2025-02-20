using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Commands;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("by-username/{username}")]
    public async Task<IActionResult> GetUserByUsername(string username)
    {
        var query = new GetUserByUsernameCommand(username);
        var user = await _mediator.Send(query);

        if (user == null)
            return NotFound("Usuário não encontrado.");

        return Ok(user);
    }
}
