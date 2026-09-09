using System.Net;

namespace Common.HttpClient;

public class HttpClientResponse<TResponse>
{
    public HttpStatusCode StatusCode { get; set; }
    public TResponse? Response { get; set; }
    public string? StringResponse { get; set; }
}




