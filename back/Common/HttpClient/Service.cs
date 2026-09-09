using System.Net.Http.Json;
using System.Net.Http.Headers;
using Microsoft.Extensions.Logging;

namespace Common.HttpClient;

public class HttpClientService(ILogger<HttpClientService> logger, System.Net.Http.HttpClient client)
{
    public async Task<HttpClientResponse<TResult>> Send<TResult>(HttpClientRequest userRequest, CancellationToken cancellation)
    {
        var result = new HttpClientResponse<TResult>();

        try
        {
            logger.LogInformationCustom($"Sending POST request to {userRequest.Url}");

            var request = new HttpRequestMessage(userRequest.Method, userRequest.Url);

            if (userRequest.Body != null)
            {
                request.Content = JsonContent.Create(userRequest.Body);
                logger.LogInformationCustom($"Body :{Util.Json.Serialize(userRequest.Body)}");
            }

            if (userRequest.Token != null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", userRequest.Token);
                logger.LogInformationCustom($"Token :{userRequest.Token}");
            }

            if (userRequest.ApiKey != null)
            {
                request.Headers.Add("XAPIKEY", userRequest.ApiKey);
                logger.LogInformationCustom($"ApiKey :{userRequest.ApiKey}");
            }


            logger.LogInformationCustom($"Request :{Util.Json.Serialize(request)}");
            var response = await client.SendAsync(request);
            logger.LogInformationCustom($"Response Code :{response.StatusCode}");

            result.StatusCode = response.StatusCode;
            result.StringResponse = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode && !string.IsNullOrEmpty(result.StringResponse))
            {
                result.Response = await response.Content.ReadFromJsonAsync<TResult>();
            }
        }
        catch (Exception exception)
        {
            logger.LogError($"Response Exception :{exception.ToString()}");
            throw new Exception("POST Request to :"+userRequest.Url,exception);
        }

        return result;
    }
}




