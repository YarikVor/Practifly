using Microsoft.OpenApi.Models;

namespace PractiFly.WebApi.Schema;

public sealed class TimeOnlyApiSchema : OpenApiSchema{
    public TimeOnlyApiSchema()
    {
        Type = "string";
        Format = "time";
    }
    
    public static TimeOnlyApiSchema Create()
    {
        return new();
    }
}