using PDH.MessagingDemo.Shared;
using Rebus.Handlers;

namespace PDH.MessagingDemo.Ui;

public class MessageHandler : IHandleMessages<ChatMessage>
{
    private readonly EventDispatcher<TestMessageReceivedEventArgs> _dispatcher;
    private readonly IServiceScopeFactory _scopeFactory;

    public MessageHandler(EventDispatcher<TestMessageReceivedEventArgs> dispatcher, IServiceScopeFactory scopeFactory)
    {
        _dispatcher = dispatcher;
        _scopeFactory = scopeFactory;
    }

    public Task Handle(ChatMessage message)
    {
        _dispatcher.MessageReceived(new TestMessageReceivedEventArgs(message.Message, message.User));
        return Task.CompletedTask;
    }
}
