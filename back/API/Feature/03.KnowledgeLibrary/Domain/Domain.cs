namespace API.Feature.Domain;

public partial class Knowledge
{
    public Knowledge(string problemTitle, long subjectId, long createdByUserId,
        IEnumerable<string> tags, DateTime? validityDate, bool isPermanently,
        KnowledgeStatus status = KnowledgeStatus.PendingReview)
    {
        ProblemTitle = RequireTitle(problemTitle);
        SubjectId = subjectId;
        CreatedByUserId = createdByUserId;
        ValidityDate = validityDate;
        IsPermanently = isPermanently;
        Status = status;
        CreatedAt = DateTime.Now;
        ReplaceTags(tags);
    }

    public void Update(string problemTitle, long subjectId, IEnumerable<string> tags,
        KnowledgeStatus status, DateTime? validityDate, bool isPermanently)
    {
        ProblemTitle = RequireTitle(problemTitle);
        SubjectId = subjectId;
        Status = status;
        ValidityDate = validityDate;
        IsPermanently = isPermanently;
        ReplaceTags(tags);
        UpdateAt = DateTime.Now;
    }

    private void ReplaceTags(IEnumerable<string> tags)
    {
        var normalized = tags.Select(x => x?.Trim()).Where(x => !string.IsNullOrWhiteSpace(x))
            .Distinct(StringComparer.OrdinalIgnoreCase).ToArray();
        if (normalized.Length == 0) throw new ArgumentException("At least one tag is required.", nameof(tags));
        Tags.Clear();
        foreach (var tag in normalized) Tags.Add(new KnowledgeTag(tag!));
    }

    private static string RequireTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Problem title is required.", nameof(title));
        return title.Trim();
    }
}
