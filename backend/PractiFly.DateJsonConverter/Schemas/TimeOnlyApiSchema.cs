using Microsoft.OpenApi.Models;

namespace PractiFly.DateJsonConverter.Schemas;

public sealed class TimeOnlyApiSchema : OpenApiSchema
{
    public TimeOnlyApiSchema()
    {
        Type = "string";
        Format = "time";
    }

    public static TimeOnlyApiSchema Create()
    {
        return new TimeOnlyApiSchema();
    }
}