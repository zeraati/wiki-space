using API.Feature.Domain;

namespace API.Feature.Endpoint;

public record KnowledgeUpdateRequest(
    long Id,
    string ProblemTitle,
    long SubjectId,
    string[] Tags,
    KnowledgeStatus Status,
    DateTime? ValidityDate,
    bool IsPermanently);
