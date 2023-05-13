using System.Security.Claims;

namespace PractiFly.WebApi.Controllers;

public static class ClaimsPrincipalEx
{
    public static string GetUserId(this ClaimsPrincipal principal)
    {
        return principal.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    public static int GetUserIdInt(this ClaimsPrincipal principal)
    {
        return int.Parse(principal.FindFirstValue(ClaimTypes.NameIdentifier));
    }
}