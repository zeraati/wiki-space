using API.Feature.Domain;

namespace API.Feature.Endpoint;
public class UserDelete(AppDbContext db): FastEndpoint<UserDeleteRequest>
{
    public override void Configure()
    {
        Delete("user");
    }

    public override async Task HandleAsync(UserDeleteRequest request, CancellationToken cancellation)
    {
        await db.Set<User>().Where(x => x.Id == request.Id).ExecuteDeleteAsync(cancellation);
    }
}