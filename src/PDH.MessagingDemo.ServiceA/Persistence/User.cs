namespace PDH.MessagingDemo.ServiceA.Persistence;

public class User
{
    public Guid Id { get; set; }

    public string ProviderId { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;
}
