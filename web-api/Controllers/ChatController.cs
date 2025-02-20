using ChatABInBev.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

[Route("api/chat")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ChatController : ControllerBase
{
    private readonly OpenAIService _openAIService;
    private readonly AppDbContext _context;

    public ChatController(OpenAIService openAIService, AppDbContext context)
    {
        _openAIService = openAIService;
        _context = context;
    }

    [HttpPost("SendMessage")]
    public async Task<IActionResult> SendMessage([FromBody] ChatMessageDto messageDto)
    {
        if (messageDto == null || string.IsNullOrEmpty(messageDto.UserMessage) || messageDto.UserId == Guid.Empty)
        {
            return BadRequest(new { message = "A mensagem do usuário não pode ser vazia e o usuário deve ser válido." });
        }

        try
        {
            var userExists = await _context.Users.AnyAsync(u => u.Id == messageDto.UserId);
            if (!userExists)
            {
                return NotFound(new { message = "Usuário não encontrado no banco de dados." });
            }

            var response = await _openAIService.GetChatGPTResponse(messageDto.UserMessage);

            var message = new ChatMessage
            {
                UserMessage = messageDto.UserMessage,
                ChatGPTResponse = response,
                UserId = messageDto.UserId
            };

            _context.ChatMessages.Add(message);
            await _context.SaveChangesAsync();

            return Ok(message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                message = "Erro ao processar a mensagem.",
                error = ex.Message,
                innerError = ex.InnerException?.Message
            });
        }
    }

    [HttpGet("GetMessages/{userId}")]
    public async Task<IActionResult> GetMessages(Guid userId)
    {
        try
        {
            var messages = await _context.ChatMessages
                .Where(m => m.UserId == userId)
                .OrderByDescending(m => m.CreatedAt)
                .ToListAsync();

            if (messages == null || messages.Count == 0)
            {
                return NotFound(new { message = "Nenhuma mensagem encontrada para este usuário." });
            }

            return Ok(messages);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro ao buscar as mensagens.", error = ex.Message });
        }
    }
}
