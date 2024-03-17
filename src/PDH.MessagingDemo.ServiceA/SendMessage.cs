using Microsoft.AspNetCore.Mvc;
using PDH.MessagingDemo.Shared;
using Rebus.Bus;

namespace PDH.MessagingDemo.ServiceA;

public class SendMessage
{
    public static async Task<IResult> Execute([FromQuery] string message, [FromQuery] string user, IBus bus)
    {
        await bus.Publish(new ChatMessage(message, user));

        return Results.Ok(Task.CompletedTask);
    }
}