
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] User user)
    {
        try
        {
            var token = _authService.GenerateJwtToken(user.Username);
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