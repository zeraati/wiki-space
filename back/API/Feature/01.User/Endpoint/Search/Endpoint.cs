using API.Feature.Domain;

namespace API.Feature.Endpoint;

public class UserSearch(AppDbContext db) : FastEndpoint<UserSearchRequest, UserGetResponse[]>
{
    public override void Configure()
    {
        Get("user/search");
    }

    public override async Task HandleAsync(UserSearchRequest request, CancellationToken cancellation)
    {
        var query = db.Set<User>().AsQueryable();
        if (request.Name != null) query = query.Where(x => x.Name.Contains(request.Name));
        var users = await query.Select(x => new UserGetResponse(x)).ToArrayAsync(cancellation);
        Response = users;
    }
}
