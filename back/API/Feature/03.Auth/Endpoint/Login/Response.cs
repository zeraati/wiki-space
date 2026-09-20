using API.Feature.Domain;

namespace API.Feature.Endpoint;

public class LoginGetResponse
{
    public UserGetResponse(User user)
    {
        Id = user.Id;
        Name = user.Name;
    }

    public long Id { get; private set; }
    public string Name { get; private set; }
}



public class LoginGetResponse<T>
{
    public T Data { get; set; }
    public bool Failed { get; set; }
    public string Message { get; set; }
    public bool Succeeded { get; set; }
    public int Code { get; set; }
}

public class TokenResponse
{
    public TokenInfo TokenInfo { get; set; }
}

public class TokenInfo
{
    public TokenMetadata Metadata { get; set; }

    public string JwtToken { get; set; }

    public DateTime IssuedOn { get; set; }

    public DateTime ExpiresOn { get; set; }

    public string RefreshToken { get; set; }

    public bool PasswordExpired { get; set; }

    public bool RequiresTwoFactorAuthentication { get; set; }

    public string TwoFactorToken { get; set; }

    public string ErrorMessage { get; set; }

    public string Mobile { get; set; }
}

public class TokenMetadata
{
    public string Username { get; set; }

    public int PersonnelId { get; set; }
}