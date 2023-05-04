using System.Security.Claims;

namespace PractiFly.WebApi.Services.TokenGenerator;

public interface ITokenGenerator
{
    string GenerateToken(IEnumerable<Claim> claims);
}