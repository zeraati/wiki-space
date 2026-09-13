using API.Feature.Domain;

namespace API.Feature.Endpoint;

public class SubjectUpdateActivation(AppDbContext db) : FastEndpoint<SubjectUpdateActivationRequest>
{
    public override void Configure()
    {
        Put("subject/activation");
    }

    public override async Task HandleAsync(SubjectUpdateActivationRequest request, CancellationToken cancellation)
    {
        var subject = await db.Set<Subject>().SingleAsync(x => x.Id == request.Id, cancellation);
        subject.UpdateActivation(request.IsActive);
        db.Set<Subject>().Update(subject);
        await db.SaveChangesAsync(cancellation);
    }
}