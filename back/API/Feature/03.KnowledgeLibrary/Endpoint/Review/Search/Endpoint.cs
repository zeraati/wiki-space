using API.Feature.Domain;

namespace API.Feature.Endpoint;

public class KnowledgeReviewSearch(AppDbContext db)
    : FastEndpoint<KnowledgeReviewSearchRequest, KnowledgeSearchResponse[]>
{
    public override void Configure() => Get("knowledge/review");

    public override async Task HandleAsync(
        KnowledgeReviewSearchRequest request,
        CancellationToken cancellation)
    {
        var query = db.Set<Knowledge>()
            .AsNoTracking()
            .Where(x => x.Status == KnowledgeStatus.PendingReview ||
                        x.Status == KnowledgeStatus.EditedPendingReview);

        if (request.Status.HasValue)
            query = query.Where(x => x.Status == request.Status.Value);

        var records = await query
            .Include(x => x.Subject)
            .Include(x => x.CreatedByUser)
            .Include(x => x.Tags)
            .OrderByDescending(x => x.UpdateAt)
            .ThenByDescending(x => x.CreatedAt)
            .ToListAsync(cancellation);

        Response = records.Select(x => new KnowledgeSearchResponse
        {
            Id = x.Id,
            ProblemTitle = x.ProblemTitle,
            SubjectId = x.SubjectId,
            SubjectTitle = x.Subject.Title,
            Tags = x.Tags.Select(tag => tag.Name).ToArray(),
            Status = x.Status,
            CreatedByUserId = x.CreatedByUserId,
            CreatedByUserName = x.CreatedByUser.Name,
            ValidityDate = x.ValidityDate,
            IsPermanently = x.IsPermanently,
            CreatedAt = x.CreatedAt,
            UpdateAt = x.UpdateAt
        }).ToArray();
    }
}
