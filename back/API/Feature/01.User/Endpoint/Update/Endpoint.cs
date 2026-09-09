using API.Feature.Domain;

namespace API.Feature.Endpoint;

public class UserUpdate(AppDbContext db) : FastEndpoint<UserUpdateRequest>
{
    public override void Configure()
    {
        Put("user");
    }

    public override async Task HandleAsync(UserUpdateRequest request, CancellationToken cancellation)
    {
        var user = await db.Set<User>().SingleAsync(x => x.Id == request.Id, cancellation);
        user.Update(request.Name);
        db.Set<User>().Update(user);
        await db.SaveChangesAsync(cancellation);
    }
}