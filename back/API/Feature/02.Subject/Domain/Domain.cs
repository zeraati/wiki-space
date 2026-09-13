namespace API.Feature.Domain;

public partial class Subject
{
    public Subject(string title)
    {
        Title = title;
    }

    public void UpdateTitle(string title)
    {
        Title = title;
        UpdateAt = DateTime.Now;
    }

    public void UpdateActivation(bool isActive)
    {
        IsActive = isActive;
        UpdateAt = DateTime.Now;
    }
}