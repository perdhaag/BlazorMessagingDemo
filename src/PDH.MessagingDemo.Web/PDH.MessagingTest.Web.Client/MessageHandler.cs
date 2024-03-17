using PDH.MessagingDemo.Shared;
using Rebus.Handlers;

namespace PDH.MessagingDemo.Web.Client;

public class MessageHandler : IHandleMessages<ChatMessage>
{
    private readonly EventDispatcher<TestMessageReceivedEventArgs> _dispatcher;

    public MessageHandler(EventDispatcher<TestMessageReceivedEventArgs> dispatcher)
    {
        _dispatcher = dispatcher;
    }

    public Task Handle(ChatMessage message)
    {
        Console.WriteLine(message.Message);
        _dispatcher.MessageReceived(new TestMessageReceivedEventArgs(message.Message, message.User));
        return Task.CompletedTask;
    }
}
