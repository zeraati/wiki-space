using API.Feature.Domain;
using Common.Extension;

namespace API.Feature.Endpoint;

public class KnowledgeCreate(AppDbContext db, IHttpContextAccessor httpContext) : FastEndpoint<KnowledgeCreateRequest, long>
{
    public override void Configure() => Post("knowledge");

    public override async Task HandleAsync(KnowledgeCreateRequest request, CancellationToken cancellation)
    {
        var userId = httpContext.UserId();
        if (userId is null) { await SendUnauthorizedAsync(cancellation); return; }
        var knowledge = new Knowledge(request.ProblemTitle, request.SubjectId, userId.Value,
            request.Tags, request.ValidityDate, request.IsPermanently);
        db.Set<Knowledge>().Add(knowledge);
        await db.SaveChangesAsync(cancellation);
        Response = knowledge.Id;
    }
}
