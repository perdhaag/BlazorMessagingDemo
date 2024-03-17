using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PDH.MessagingDemo.ServiceA.Persistence;

namespace PDH.MessagingDemo.ServiceA.Endpoints
{
    public static class EndpointsRegistration
    {
        public static void MapEndpoints(this WebApplication app)
        {
            app.MapPost("/message", SendMessage.Execute);
            app.MapPost("/chatroom", CreateChatRoom.Execute);
            app.MapGet("chatroom/{id}", async ([FromRoute] Guid id, ChatContext db) =>
            {
                var messages = await db.ChatMessages
                    .Where(x => x.ChatRoom.Id == id)
                    .ToListAsync();

                return Results.Ok(messages.Select(x => new { Message = x.Message, User = x.User }));
            });
            app.MapPost("/user", CreateUser.Execute);
            app.MapGet("/user", GetAllUsers.Execute);
        }
    }
}
