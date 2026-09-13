using API.Feature.Domain;

namespace API.Feature.Endpoint;

public class KnowledgeSearch(AppDbContext db) : FastEndpoint<KnowledgeSearchRequest, KnowledgeSearchResponse[]>
{
    public override void Configure() => Get("knowledge/search");

    public override async Task HandleAsync(KnowledgeSearchRequest request, CancellationToken cancellation)
    {
        var query = db.Set<Knowledge>()
            .AsNoTracking()
            .ApplyFilters(request);

        var records = await query.Include(x => x.Subject).Include(x => x.CreatedByUser).Include(x => x.Tags)
            .OrderByDescending(x => x.CreatedAt).ToListAsync(cancellation);
        Response = records.Select(x => new KnowledgeSearchResponse
        {
            Id = x.Id,
            ProblemTitle = x.ProblemTitle,
            SubjectId = x.SubjectId,
            SubjectTitle = x.Subject.Title,
            Tags = x.Tags.Select(t => t.Name).ToArray(),
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
