using API.Feature.Domain;

namespace API.Feature.Endpoint;

public record KnowledgeSearchRequest(
    string? ProblemTitle,
    long? SubjectId,
    string? Tag,
    KnowledgeStatus? Status,
    long? CreatedByUserId,
    bool? IsPermanently,
    DateTime? ValidityFrom,
    DateTime? ValidityTo,
    DateTime? CreatedFrom = null,
    DateTime? CreatedTo = null,
    DateTime? UpdatedFrom = null,
    DateTime? UpdatedTo = null);
