using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PDH.MessagingDemo.ServiceA.Persistence;

namespace PDH.MessagingDemo.ServiceA.Endpoints
{
    public class CreateUser
    {
        public record Request(string ProviderId, string Username, string Email);

        public static async Task<IResult> Execute([FromBody] Request request, ChatContext db)
        {
            var user = await db.Users.FirstOrDefaultAsync(x => x.UserName == request.Username);
            if (user is not null) return Results.Ok();

            db.Users.Add(new User()
            {
                Id = Guid.NewGuid(),
                ProviderId = request.ProviderId,
                UserName = request.Username,
                Email = request.Email
            });
            await db.SaveChangesAsync();

            return Results.Ok();
        }
    }
}
