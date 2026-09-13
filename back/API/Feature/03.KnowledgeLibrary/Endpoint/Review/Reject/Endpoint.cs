using API.Feature.Domain;
using Common.Extension;

namespace API.Feature.Endpoint;

public class KnowledgeReject(AppDbContext db, IHttpContextAccessor httpContext)
    : FastEndpoint<KnowledgeRejectRequest>
{
    public override void Configure() => Post("knowledge/review/reject");

    public override async Task HandleAsync(
        KnowledgeRejectRequest request,
        CancellationToken cancellation)
    {
        if (string.IsNullOrWhiteSpace(request.Reason))
            throw new ArgumentException("A reason is required when rejecting knowledge.", nameof(request.Reason));

        var reviewerId = httpContext.UserId();
        if (reviewerId is null)
        {
            await SendUnauthorizedAsync(cancellation);
            return;
        }

        var knowledge = await db.Set<Knowledge>().SingleAsync(x => x.Id == request.Id, cancellation);
        knowledge.Reject();
        db.Set<KnowledgeReviewHistory>().Add(new KnowledgeReviewHistory(
            knowledge.Id, reviewerId.Value, KnowledgeReviewAction.Rejected, request.Reason));
        await db.SaveChangesAsync(cancellation);
    }
}
