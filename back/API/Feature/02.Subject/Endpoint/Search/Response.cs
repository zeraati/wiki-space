using API.Feature.Domain;

namespace API.Feature.Endpoint;

public class SubjectGetResponse
{
    public SubjectGetResponse(Subject subject)
    {
        Id = subject.Id;
        Title = subject.Title;
        IsActive = subject.IsActive;
    }

    public long Id { get; private set; }
    public string Title { get; private set; }
    public bool IsActive { get; private set; }
}
