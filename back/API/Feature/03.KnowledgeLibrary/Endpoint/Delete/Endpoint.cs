using API.Feature.Domain;

namespace API.Feature.Endpoint;

public class KnowledgeDelete(AppDbContext db) : FastEndpoint<KnowledgeDeleteRequest>
{
    public override void Configure() => Delete("knowledge");

    public override async Task HandleAsync(KnowledgeDeleteRequest request, CancellationToken cancellation)
        => await db.Set<Knowledge>().Where(x => x.Id == request.Id).ExecuteDeleteAsync(cancellation);
}
