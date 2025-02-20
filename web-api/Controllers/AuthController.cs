
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    private readonly IUserRepository _userRepository;

    public AuthController(AuthService authService, IUserRepository userRepository)
    {
        _authService = authService;
        _userRepository = userRepository;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] User request)
    {
        try
        {
            var user = await _userRepository.GetUserByUsernameAsync(request.Username);

            var token = _authService.GenerateJwtToken(user);
            return Ok(new { token });
        }
        catch (Exception ex)
        {
            return Unauthorized(ex);
        }
    }

    [HttpGet("protected")]
    [Authorize]
    public IActionResult Protected()
    {
        return Ok(new { message = "Este é um endpoint protegido!" });
    }
}