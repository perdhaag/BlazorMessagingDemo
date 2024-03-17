using Microsoft.EntityFrameworkCore;

namespace PDH.MessagingDemo.ServiceA.Persistence;

public class ChatContext : DbContext
{
    public ChatContext(DbContextOptions<ChatContext> options) : base(options) { }

    public DbSet<ChatMessage> ChatMessages { get; set; }

    public DbSet<ChatRoom> ChatRooms { get; set; }

    public DbSet<ChatRoomUser> ChatRoomUsers { get; set; }

    public DbSet<User> Users { get; set; }
}
