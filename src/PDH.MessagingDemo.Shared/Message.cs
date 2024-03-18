namespace PDH.MessagingDemo.Shared;
public class ChatMessage
{
    public ChatMessage(string message, string user, ChatRoomInfo chatRoom)
    {
        Id = Guid.NewGuid();
        Message = message;
        User = user;
        ChatRoom = chatRoom;
    }

    public Guid Id { get; set; }

    public ChatRoomInfo ChatRoom { get; set; }

    public string Message { get; set; }

    public string User { get; set; }
}

public class ChatRoomInfo
{
    public Guid Id { get; set; }

    public IEnumerable<string> Users { get; set; } = null!;
}
