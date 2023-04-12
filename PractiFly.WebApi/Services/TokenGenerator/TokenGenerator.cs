using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using PractiFly.WebApi.Services.AuthenticationOptions;

namespace PractiFly.WebApi.Services.TokenGenerator;

public class TokenGenerator : ITokenGenerator
{
    private readonly IAuthOptions _authOptions;

    private readonly JwtSecurityTokenHandler _handler = new();

    public TokenGenerator(IAuthOptions authOptions)
    {
        _authOptions = authOptions;
    }

    public string GenerateToken(IEnumerable<Claim> claims)
    {
        var token = new JwtSecurityToken(
            _authOptions.Issuer,
            _authOptions.Audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(5),
            signingCredentials: new SigningCredentials(
                _authOptions.SymmetricSecurityKey,
                SecurityAlgorithms.HmacSha256
            )
        );

        return _handler.WriteToken(token);
    }

    // TODO: Refresh tokens
}