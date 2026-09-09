namespace API.Feature.Endpoint;

public record UserSearchRequest(string? Name,long? ParentId, ProductionFormulaType? FormulaType);
