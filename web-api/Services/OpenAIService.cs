using System.Text.Json;
using System.Text.Json.Serialization;


public class OpenAIService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public OpenAIService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _apiKey = configuration["OpenAI:ApiKey"];
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _apiKey);
    }

    public async Task<string> GetChatGPTResponse(string userMessage)
    {
        var request = new
        {
            model = "gpt-4o-mini",
            messages = new[]
            {
                new { role = "system", content = "Você é um assistente especializado em responder sobre a empresa AB InBev." },
                new { role = "user", content = userMessage }
            }
        };

        var response = await _httpClient.PostAsJsonAsync($"https://api.openai.com/v1/chat/completions", request);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Erro na requisição: {response.StatusCode} - {errorContent}");
        }

        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonSerializer.Deserialize<ChatGPTResponse>(responseString);
        return result.Choices.First().Message.Content;
    }
}

public class ChatGPTResponse
{
    [JsonPropertyName("choices")]
    public List<Choice> Choices { get; set; }
}

public class Choice
{
    [JsonPropertyName("message")]
    public Message Message { get; set; }
}

public class Message
{
    [JsonPropertyName("content")]
    public string Content { get; set; }
}