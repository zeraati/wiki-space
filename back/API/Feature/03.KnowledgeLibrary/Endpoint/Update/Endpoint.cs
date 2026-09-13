using API.Feature.Domain;

namespace API.Feature.Endpoint;

public class KnowledgeUpdate(AppDbContext db) : FastEndpoint<KnowledgeUpdateRequest>
{
    public override void Configure() => Put("knowledge");

    public override async Task HandleAsync(KnowledgeUpdateRequest request, CancellationToken cancellation)
    {
        var knowledge = await db.Set<Knowledge>().Include(x => x.Tags)
            .SingleAsync(x => x.Id == request.Id, cancellation);
        knowledge.Update(request.ProblemTitle, request.SubjectId, request.Tags, request.Status,
            request.ValidityDate, request.IsPermanently);
        await db.SaveChangesAsync(cancellation);
    }
}
