using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace PractiFly.WebApi.Services.AuthenticationOptions;

public class AuthOptions : IAuthOptions
{
    private const string SectionName = "Secret";
    private const string IssuerKey = "Issuer";
    private const string AudienceKey = "Audience";
    private const string SecretKey = "Key";
    private const string TimeLifeKey = "TimeLife";


    private readonly IConfigurationSection _section;

    public AuthOptions(IConfiguration configuration)
    {
        _section = configuration.GetSection(SectionName)
                   ?? throw new NullReferenceException(SectionNotFound());
    }

    public string Issuer => _section[IssuerKey]
                            ?? throw new NullReferenceException(ValueNotFound(IssuerKey));

    public string Audience => _section[AudienceKey]
                              ?? throw new NullReferenceException(ValueNotFound(AudienceKey));

    public string Secret => _section[SecretKey]
                            ?? throw new NullReferenceException(ValueNotFound(SecretKey));

    public int TimeLife => int.Parse(
        _section[TimeLifeKey]
        ?? throw new NullReferenceException(ValueNotFound(TimeLifeKey))
    );


    public SecurityKey SymmetricSecurityKey
    {
        get
        {
            var encodingSecret = Encoding.UTF8.GetBytes(Secret);
            if (encodingSecret.Length < 16)
            {
                var newSecret = new byte[16];
                Array.Copy(encodingSecret, newSecret, encodingSecret.Length);
                encodingSecret = newSecret;
            }

            return new SymmetricSecurityKey(encodingSecret);
        }
    }

    public string ValueNotFound(string key)
    {
        return $"Key {key} not found in {SectionName} section";
    }

    public string SectionNotFound()
    {
        return $"Section {SectionName} not found in configuration";
    }
}