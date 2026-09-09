namespace API.Feature.Domain;

public partial class User
{
    public User(string name)
    {
        Name = name;
    }

    public void Update(string name)
    {
        Name = name;
        UpdateAt = DateTime.Now;
    }
}