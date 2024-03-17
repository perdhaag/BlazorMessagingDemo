namespace PDH.MessagingDemo.ServiceA.Persistence
{
    public class ChatRoomUser
    {
        public Guid Id { get; set; }

        public string ProviderId { get; set; } = null!;

        public string Email { get; set; } = null!;
    }
}
