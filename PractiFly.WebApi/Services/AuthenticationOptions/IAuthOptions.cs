using Microsoft.IdentityModel.Tokens;

namespace PractiFly.WebApi.Services.AuthenticationOptions;

public interface IAuthOptions
{
    string Issuer { get; }
    string Audience { get; }
    string Secret { get; }
    SecurityKey SymmetricSecurityKey { get; }
}