using PDH.MessagingDemo.Shared;
using Rebus.Handlers;

namespace PDH.MessagingDemo.ServiceB;

public class MessageHandler : IHandleMessages<ChatMessage>
{
    public Task Handle(ChatMessage message)
    {
        Console.WriteLine(message.Message);
        return Task.CompletedTask;
    }
}