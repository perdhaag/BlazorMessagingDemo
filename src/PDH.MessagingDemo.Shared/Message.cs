namespace PDH.MessagingDemo.Shared;
public class ChatMessage
{
    public ChatMessage(string message, string user, ChatRoomInfo chatRoomId)
    {
        Id = Guid.NewGuid();
        Message = message;
        User = user;
        ChatRoomId = chatRoomId;
    }

    public Guid Id { get; set; }

    public ChatRoomInfo ChatRoomId { get; set; }

    public string Message { get; set; }

    public string User { get; set; }
}

public class ChatRoomInfo
{
    public Guid Id { get; set; }

    public IEnumerable<string> Users { get; set; } = null!;
}
