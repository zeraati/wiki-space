namespace Common.HttpClient;

public class HttpClientRequest
{
    public HttpClientRequest(HttpMethod method,string url)
    {
            Method= method;
            Url= url;
    }

    public void SetBody(object body) => Body = body;
    public void SetToken(string token) => Token = token;
    public void SetApiKey(string apiKey) => ApiKey = apiKey;

    public HttpMethod Method { get; set; }
    public string Url { get; set; }
    public object? Body { get; set; }
    public string? Token { get; set; }
    public string? ApiKey { get; set; }
}




