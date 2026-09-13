namespace API.Feature.Endpoint;

public record KnowledgeCreateRequest(
    string ProblemTitle,
    long SubjectId,
    string[] Tags,
    DateTime? ValidityDate,
    bool IsPermanently);