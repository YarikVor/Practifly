using Microsoft.OpenApi.Models;

namespace PractiFly.DateJsonConverter.Schemas;

public sealed class DateOnlyApiSchema : OpenApiSchema
{
    public DateOnlyApiSchema()
    {
        Type = "string";
        Format = "date";
    }

    public static DateOnlyApiSchema Create()
    {
        return new DateOnlyApiSchema();
    }
}