namespace PDH.MessagingDemo.ServiceA.Persistence;
public class ChatRoom
{
    public Guid Id { get; set; }

    public ICollection<ChatMessage>? Messages { get; set; }

    public ICollection<ChatRoomUser>? Users { get; set; }
}

