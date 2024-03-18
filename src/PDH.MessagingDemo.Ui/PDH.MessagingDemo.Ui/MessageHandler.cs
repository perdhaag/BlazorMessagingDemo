using PDH.MessagingDemo.Shared;
using PDH.MessagingDemo.Ui.Client;
using Rebus.Handlers;

namespace PDH.MessagingDemo.Ui;

public class MessageHandler(MessageReciever messageReciever) : IHandleMessages<ChatMessage>
{
    public async Task Handle(ChatMessage message)
    {
        await messageReciever.SendMessage(message);
    }
}
