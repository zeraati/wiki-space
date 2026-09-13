using API.Feature.Domain;

namespace API.Feature.Endpoint;

internal static class KnowledgeSearchQueryExtensions
{
    public static IQueryable<Knowledge> ApplyFilters(
        this IQueryable<Knowledge> query,
        KnowledgeSearchRequest request)
    {
        if (!string.IsNullOrWhiteSpace(request.ProblemTitle))
            query = query.Where(x => x.ProblemTitle.Contains(request.ProblemTitle));

        if (request.SubjectId.HasValue)
            query = query.Where(x => x.SubjectId == request.SubjectId.Value);

        if (!string.IsNullOrWhiteSpace(request.Tag))
            query = query.Where(x => x.Tags.Any(tag => tag.Name.Contains(request.Tag)));

        if (request.Status.HasValue)
            query = query.Where(x => x.Status == request.Status.Value);

        if (request.CreatedByUserId.HasValue)
            query = query.Where(x => x.CreatedByUserId == request.CreatedByUserId.Value);

        if (request.IsPermanently.HasValue)
            query = query.Where(x => x.IsPermanently == request.IsPermanently.Value);

        if (request.ValidityFrom.HasValue)
            query = query.Where(x => x.ValidityDate >= request.ValidityFrom.Value);

        if (request.ValidityTo.HasValue)
            query = query.Where(x => x.ValidityDate <= request.ValidityTo.Value);

        if (request.CreatedFrom.HasValue)
            query = query.Where(x => x.CreatedAt >= request.CreatedFrom.Value);

        if (request.CreatedTo.HasValue)
            query = query.Where(x => x.CreatedAt <= request.CreatedTo.Value);

        if (request.UpdatedFrom.HasValue)
            query = query.Where(x => x.UpdateAt >= request.UpdatedFrom.Value);

        if (request.UpdatedTo.HasValue)
            query = query.Where(x => x.UpdateAt <= request.UpdatedTo.Value);

        return query;
    }
}
