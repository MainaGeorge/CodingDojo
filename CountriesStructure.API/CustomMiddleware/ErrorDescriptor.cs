using Newtonsoft.Json;

namespace CountriesStructure.API.CustomMiddleware;

public class ErrorDescriptor
{
    public int StatusCode { get; private set; }
    public string Message { get; private set; }

    public string? StackTrace { get; private set; }

    public ErrorDescriptor(int statusCode, string message, string? stackTrace = null)
    {
        StackTrace = stackTrace;
        StatusCode = statusCode;
        Message = message;
    }


    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}