using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PDH.MessagingDemo.ServiceA.Persistence;

namespace PDH.MessagingDemo.ServiceA.Endpoints;

public class CreateChatRoom
{
    public record Request(IEnumerable<string> Users);

    public static async Task<IResult> Execute([FromBody] Request request, ChatContext db)
    {
        var users = await db.Users
            .Where(x => request.Users.Contains(x.UserName))
            .ToListAsync();

        var room = db.ChatRooms.Add(new ChatRoom
        {
            Id = Guid.NewGuid(),
            Users = users.Select(x => new ChatRoomUser
            {
                Id = Guid.NewGuid(),
                ProviderId = x.ProviderId,
                Email = x.Email
            }).ToList(),
            Messages = null
        });
        await db.SaveChangesAsync();
        return Results.Ok(room.Entity.Id);
    }
}
