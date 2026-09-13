using API.Feature.Domain;

namespace API.Feature.Endpoint;
public class SubjectDelete(AppDbContext db): FastEndpoint<SubjectDeleteRequest>
{
    public override void Configure()
    {
        Delete("subject");
    }

    public override async Task HandleAsync(SubjectDeleteRequest request, CancellationToken cancellation)
    {
        await db.Set<Subject>().Where(x => x.Id == request.Id).ExecuteDeleteAsync(cancellation);
    }
}