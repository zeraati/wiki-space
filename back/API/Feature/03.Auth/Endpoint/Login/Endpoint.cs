namespace API.Feature.Endpoint;

public class AuthLogin : FastEndpointWithoutRequest<object>
{
    public override void Configure()
    {
        Post("auth/login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken cancellation)
    {
        Response = new
        {
            Data = new
            {
                TokenInfo = new
                {
                    Metadata = new
                    {
                        Username = "mortazavi",
                        PersonnelId = 990962
                    },

                    JwtToken = "eyJhbGciOiJFUzM4NCIsInR5cCI6IkpXVCJ9...",

                    IssuedOn = DateTime.Parse("2026-09-19T16:36:38"),
                    ExpiresOn = DateTime.Parse("2026-09-19T17:06:38"),

                    RefreshToken = "6EBB372034B3215366C41357B8AE3DA16FCCD63D91BC7DD3E9DADF3CF16E762C000CB09FD79C5FA97299DFA1BA5937F884BEA39A1F488A32E547F4B911F6E207",

                    PasswordExpired = false,
                    RequiresTwoFactorAuthentication = false
                }
            },

            Failed = false,
            Succeeded = true,
            Code = 0
        };
    }
}
