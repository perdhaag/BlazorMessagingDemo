using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PDH.MessagingDemo.ServiceA.Persistence;
using PDH.MessagingDemo.Shared;
using Rebus.Bus;
using ChatMessage = PDH.MessagingDemo.ServiceA.Persistence.ChatMessage;

namespace PDH.MessagingDemo.ServiceA.Endpoints;

public class SendMessage
{
    public record Request(Guid Id, string Message, string User);

    public static async Task<IResult> Execute([FromBody] Request request, IBus bus, ChatContext db)
    {
        var chatRoom = await db.ChatRooms
            .Include(x => x.Users)
            .FirstOrDefaultAsync(x => x.Id == request.Id);

        if (chatRoom is null) return Results.NotFound("Chat room not found");

        db.ChatMessages.Add(new ChatMessage
        {
            Id = Guid.NewGuid(),
            ChatRoom = chatRoom,
            Message = request.Message,
            User = request.User
        });

        await db.SaveChangesAsync();

        await bus.Publish(new Shared.ChatMessage(request.Message, request.User, new ChatRoomInfo
        {
            Id = chatRoom.Id,
            Users = chatRoom.Users?.Select(x => x.Email)!
        }));

        return Results.Ok(Task.CompletedTask);
    }
}