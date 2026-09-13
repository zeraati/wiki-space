using Common.Entity;

namespace API.Feature.Domain;

public enum KnowledgeStatus
{
    PendingReview = 1,
    Approved = 2,
    NeedsRevision = 3,
    Rejected = 4,
    EditedPendingReview = 5,
    Expired = 6
}

public partial class Knowledge : BaseEntity
{
    private Knowledge() { }

    public string ProblemTitle { get; private set; } = null!;
    public long SubjectId { get; private set; }
    public Subject Subject { get; private set; } = null!;
    public long CreatedByUserId { get; private set; }
    public User CreatedByUser { get; private set; } = null!;
    public KnowledgeStatus Status { get; private set; }
    public DateTime? ValidityDate { get; private set; }
    public bool IsPermanently { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public ICollection<KnowledgeTag> Tags { get; private set; } = new List<KnowledgeTag>();
}

public class KnowledgeTag
{
    private KnowledgeTag() { }
    public long Id { get; private set; }
    public long KnowledgeId { get; private set; }
    public Knowledge Knowledge { get; private set; } = null!;
    public string Name { get; private set; } = null!;

    public KnowledgeTag(string name) => Name = name;
}

public class KnowledgeConfig : IEntityTypeConfiguration<Knowledge>
{
    public void Configure(EntityTypeBuilder<Knowledge> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.ProblemTitle).IsRequired().HasMaxLength(2000);
        builder.Property(x => x.Status).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.HasOne(x => x.Subject).WithMany().HasForeignKey(x => x.SubjectId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(x => x.CreatedByUser).WithMany().HasForeignKey(x => x.CreatedByUserId).OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(x => x.Tags).WithOne(x => x.Knowledge).HasForeignKey(x => x.KnowledgeId).OnDelete(DeleteBehavior.Cascade);
    }
}

public class KnowledgeTagConfig : IEntityTypeConfiguration<KnowledgeTag>
{
    public void Configure(EntityTypeBuilder<KnowledgeTag> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
        builder.HasIndex(x => new { x.KnowledgeId, x.Name }).IsUnique();
    }
}
