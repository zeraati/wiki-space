using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Common.Extension;

public static class HttpResponseExtension
{
    public static async Task SendErrors(this IPreProcessorContext context, IEnumerable<ValidationError> errors, CancellationToken cancellation)
    {
        if (errors?.Any() == true)
        {
            var content = errors?.Select(x => $"'{x.Value}' {x.Message}").ToArray();
            await context.HttpContext.Response.SendAsync(content, StatusCodes.Status400BadRequest, cancellation: cancellation);
        }
    }

    public static TService Resolve<TService>(this IPreProcessorContext context) where TService : class
    {
        return context.HttpContext.Resolve<TService>();
    }
}

public record ValidationError(string Value, string Message);