using API.Feature.Domain;
using Common.Extension;

namespace API.Feature.Endpoint;

public class KnowledgeApprove(AppDbContext db, IHttpContextAccessor httpContext)
    : FastEndpoint<KnowledgeApproveRequest>
{
    public override void Configure() => Post("knowledge/review/approve");

    public override async Task HandleAsync(
        KnowledgeApproveRequest request,
        CancellationToken cancellation)
    {
        var reviewerId = httpContext.UserId();
        if (reviewerId is null)
        {
            await SendUnauthorizedAsync(cancellation);
            return;
        }

        var knowledge = await db.Set<Knowledge>().SingleAsync(x => x.Id == request.Id, cancellation);
        knowledge.Approve();
        db.Set<KnowledgeReviewHistory>().Add(new KnowledgeReviewHistory(
            knowledge.Id, reviewerId.Value, KnowledgeReviewAction.Approved, null));
        await db.SaveChangesAsync(cancellation);
    }
}
