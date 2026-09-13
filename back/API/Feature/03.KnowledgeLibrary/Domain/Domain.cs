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
        DateTime? validityDate, bool isPermanently)
    {
        if (Status == KnowledgeStatus.Approved)
            throw new InvalidOperationException("Approved knowledge can only be edited through the review workflow.");

        ProblemTitle = RequireTitle(problemTitle);
        SubjectId = subjectId;
        ValidityDate = validityDate;
        IsPermanently = isPermanently;
        ReplaceTags(tags);
        if (Status == KnowledgeStatus.NeedsRevision)
            Status = KnowledgeStatus.EditedPendingReview;
        UpdateAt = DateTime.Now;
    }

    public void Approve()
    {
        EnsureReviewable();
        Status = KnowledgeStatus.Approved;
        UpdateAt = DateTime.Now;
    }

    public void RequestRevision()
    {
        EnsureReviewable();
        Status = KnowledgeStatus.NeedsRevision;
        UpdateAt = DateTime.Now;
    }

    public void Reject()
    {
        EnsureReviewable();
        Status = KnowledgeStatus.Rejected;
        UpdateAt = DateTime.Now;
    }

    private void EnsureReviewable()
    {
        if (Status is not (KnowledgeStatus.PendingReview or KnowledgeStatus.EditedPendingReview))
            throw new InvalidOperationException("Only knowledge awaiting review can be reviewed.");
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
