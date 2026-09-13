namespace API.Feature.Endpoint;

public record KnowledgeUpdateRequest(
    long Id,
    string ProblemTitle,
    long SubjectId,
    string[] Tags,
    DateTime? ValidityDate,
    bool IsPermanently);
