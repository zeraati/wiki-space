using Common.Entity;

namespace API.Feature.Domain;

public partial class Subject : BaseEntity
{
    private Subject() { }
    public string Title { get; private set; } = null!;
    public bool IsActive { get; private set; } = true;
}

public class SubjectConfig : IEntityTypeConfiguration<Subject>
{
    public void Configure(EntityTypeBuilder<Subject> builder)
    {
    }
}