using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/database")]
[ApiController]
public class DatabaseController : ControllerBase
{
    private readonly AppDbContext _context;

    public DatabaseController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("test-connection")]
    public async Task<IActionResult> TestConnection()
    {
        try
        {
            var canConnect = await _context.Database.CanConnectAsync();
            if (canConnect)
                return Ok(new { message = "Conexão com o banco de dados bem-sucedida!" });

            return BadRequest(new { message = "Falha na conexão com o banco de dados." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro ao conectar ao banco.", error = ex.Message });
        }
    }
}
