using PDH.MessagingDemo.Shared;

namespace PDH.MessagingDemo.Ui.Client;

public class MessageReciever
{
    private readonly EventDispatcher<ChatMessage> _dispatcher;

    private readonly IServiceProvider _provider;
    public MessageReciever(EventDispatcher<ChatMessage> dispatcher, IServiceProvider provider)
    {
        _dispatcher = dispatcher;
        _provider = provider;
    }

    public Task SendMessage(Shared.ChatMessage message)
    {
        using var scope = _provider.CreateScope();
        var userStore = scope.ServiceProvider.GetRequiredService<UserStore>();
        if (!message.ChatRoom.Users.Contains(userStore.Id)) return Task.CompletedTask;
        _dispatcher.MessageReceived(message);
        return Task.CompletedTask;
    }
}
