public class ChatMessage
{
    public int Id { get; set; }
    public string UserMessage { get; set; }
    public string ChatGPTResponse { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
