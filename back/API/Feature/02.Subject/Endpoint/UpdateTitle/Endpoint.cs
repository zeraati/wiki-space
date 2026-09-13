using API.Feature.Domain;

namespace API.Feature.Endpoint;

public class SubjectUpdateTitle(AppDbContext db) : FastEndpoint<SubjectUpdateTitleRequest>
{
    public override void Configure()
    {
        Put("subject/title");
    }

    public override async Task HandleAsync(SubjectUpdateTitleRequest request, CancellationToken cancellation)
    {
        var subject = await db.Set<Subject>().SingleAsync(x => x.Id == request.Id, cancellation);
        subject.UpdateTitle(request.Title);
        db.Set<Subject>().Update(subject);
        await db.SaveChangesAsync(cancellation);
    }
}