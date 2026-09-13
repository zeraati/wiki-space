using API.Feature.Domain;

namespace API.Feature.Endpoint;

public class SubjectSearch(AppDbContext db) : FastEndpoint<SubjectSearchRequest, SubjectGetResponse[]>
{
    public override void Configure()
    {
        Get("subject/search");
    }

    public override async Task HandleAsync(SubjectSearchRequest request, CancellationToken cancellation)
    {
        var query = db.Set<Subject>().AsQueryable();
        if (request.Title != null) query = query.Where(x => x.Title.Contains(request.Title));
        if (request.IsActive != null) query = query.Where(x => x.IsActive == request.IsActive);
        var subjects = await query.Select(x => new SubjectGetResponse(x)).ToArrayAsync(cancellation);
        Response = subjects;
    }
}
