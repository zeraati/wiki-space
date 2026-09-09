namespace Common.Entity;
public class BaseEntity
{
    public long Id { get; set; }
    public DateTime UpdateAt { get; protected set; } =DateTime.Now;
}