namespace PDH.MessagingDemo.ServiceA.Persistence;
public class ChatMessage
{
    public Guid Id { get; set; }

    public string Message { get; set; } = null!;

    public string User { get; set; } = null!;

    public ChatRoom ChatRoom { get; set; } = null!;
}
