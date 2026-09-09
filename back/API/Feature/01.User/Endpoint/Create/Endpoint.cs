using FastEndpoint;
using API.Feature.Domain;

namespace API.Feature.Endpoint;

public class UserCreate(AppDbContext db) : FastEndpoint<UserCreateRequest, long>
{
    public override void Configure()
    {
        Post("user");
    }

    public override async Task HandleAsync(UserCreateRequest request, CancellationToken cancellation)
    {
        var user = new User(request.Name);
        db.Set<User>().Add(user);
        await db.SaveChangesAsync(cancellation);
        Response = user.Id;
    }
}