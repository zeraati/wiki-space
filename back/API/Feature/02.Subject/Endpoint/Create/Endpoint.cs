using API.Feature.Domain;

namespace API.Feature.Endpoint;

public class SubjectCreate(AppDbContext db) : FastEndpoint<SubjectCreateRequest, long>
{
    public override void Configure()
    {
        Post("subject");
        AllowAnonymous();
    }

    public override async Task HandleAsync(SubjectCreateRequest request, CancellationToken cancellation)
    {
        var subject = new Subject(request.Title);
        db.Set<Subject>().Add(subject);
        await db.SaveChangesAsync(cancellation);
        Response = subject.Id;
    }
}