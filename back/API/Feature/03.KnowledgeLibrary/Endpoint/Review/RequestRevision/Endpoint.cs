using API.Feature.Domain;
using Common.Extension;

namespace API.Feature.Endpoint;

public class KnowledgeRequestRevision(AppDbContext db, IHttpContextAccessor httpContext)
    : FastEndpoint<KnowledgeRequestRevisionRequest>
{
    public override void Configure() => Post("knowledge/review/request-revision");

    public override async Task HandleAsync(
        KnowledgeRequestRevisionRequest request,
        CancellationToken cancellation)
    {
        if (string.IsNullOrWhiteSpace(request.Reason))
            throw new ArgumentException("A reason is required when requesting a revision.", nameof(request.Reason));

        var reviewerId = httpContext.UserId();
        if (reviewerId is null)
        {
            await SendUnauthorizedAsync(cancellation);
            return;
        }

        var knowledge = await db.Set<Knowledge>().SingleAsync(x => x.Id == request.Id, cancellation);
        knowledge.RequestRevision();
        db.Set<KnowledgeReviewHistory>().Add(new KnowledgeReviewHistory(
            knowledge.Id, reviewerId.Value, KnowledgeReviewAction.RevisionRequested, request.Reason));
        await db.SaveChangesAsync(cancellation);
    }
}
