using Microsoft.OpenApi.Models;

namespace PractiFly.WebApi.Schema;

public sealed class DateOnlyApiSchema : OpenApiSchema
{
    public DateOnlyApiSchema()
    {
        Type = "string";
        Format = "date";
    }

    public static DateOnlyApiSchema Create()
    {
        return new();
    }
}