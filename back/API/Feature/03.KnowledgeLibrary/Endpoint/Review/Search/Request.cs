using API.Feature.Domain;

namespace API.Feature.Endpoint;

public record KnowledgeReviewSearchRequest(KnowledgeStatus? Status = null);
