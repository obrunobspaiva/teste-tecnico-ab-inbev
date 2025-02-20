using ChatABInBev.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

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

    [HttpPost]
    public async Task<IActionResult> SendMessage([FromBody] ChatMessageDto messageDto)
    {
        if (messageDto == null || string.IsNullOrEmpty(messageDto.UserMessage))
        {
            return BadRequest(new { message = "A mensagem do usuário não pode ser vazia." });
        }

        try
        {
            var response = await _openAIService.GetChatGPTResponse(messageDto.UserMessage);
            
            var message = new ChatMessage
            {
                UserMessage = messageDto.UserMessage,
                ChatGPTResponse = response
            };

            _context.ChatMessages.Add(message);
            await _context.SaveChangesAsync();

            return Ok(message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro ao processar a mensagem.", error = ex.Message });
        }
    }
}
