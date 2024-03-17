namespace PDH.MessagingDemo.Shared;
public class ChatMessage
{
    public ChatMessage(string message, string user)
    {
        Id = Guid.NewGuid();
        Message = message;
        User = user;
    }

    public Guid Id { get; set; }

    public string Message { get; set; }

    public string User { get; set; }
}
