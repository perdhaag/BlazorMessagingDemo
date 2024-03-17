using Microsoft.EntityFrameworkCore;
using PDH.MessagingDemo.ServiceA.Persistence;

namespace PDH.MessagingDemo.ServiceA.Endpoints
{
    public class GetAllUsers
    {
        public static async Task<IResult> Execute(ChatContext db)
        {
            var users = await db.Users.ToListAsync();
            return Results.Ok(users.Select(x => x.UserName));
        }
    }
}
