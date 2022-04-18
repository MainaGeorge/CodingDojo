using Newtonsoft.Json;

namespace CountriesStructure.API.CustomMiddleware;

public class ErrorDescriptor
{
    public int StatusCode { get; set; }
    public string Message { get; set; }

    public ErrorDescriptor(int statusCode, string message)
    {
        StatusCode = statusCode;
        Message = message;
    }


    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}