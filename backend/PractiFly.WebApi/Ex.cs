using System.Net;

namespace PractiFly.WebApi;

public static class Ex
{
    public static bool IsSuccess(this HttpStatusCode code)
    {
        return code is >= HttpStatusCode.OK and < HttpStatusCode.Ambiguous;
    }
}