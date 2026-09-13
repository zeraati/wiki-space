using API.Feature.Domain;

namespace API.Feature.Endpoint;

public sealed class KnowledgeSearchResponse
{
    public long Id { get; init; }
    public string ProblemTitle { get; init; } = null!;
    public long SubjectId { get; init; }
    public string SubjectTitle { get; init; } = null!;
    public string[] Tags { get; init; } = [];
    public KnowledgeStatus Status { get; init; }
    public long CreatedByUserId { get; init; }
    public string CreatedByUserName { get; init; } = null!;
    public DateTime? ValidityDate { get; init; }
    public bool IsPermanently { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime UpdateAt { get; init; }
}
