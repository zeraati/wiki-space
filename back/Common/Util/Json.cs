using System.Text.Json;

namespace Common.Util;

public static class Json
{
    public static string? Serialize(object? obj)
    {
        try
        {
            return obj != null ? JsonSerializer.Serialize(obj) : null;
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.Message);
        }

        return "Json Serialize Exception";
    }
}