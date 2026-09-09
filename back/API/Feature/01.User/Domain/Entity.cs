using Common.Entity;

namespace API.Feature.Domain;

public partial class User : BaseEntity
{
    private User() { }
    public string Name { get; private set; } = null!;
}

public class ProductCategoryConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
    }
}