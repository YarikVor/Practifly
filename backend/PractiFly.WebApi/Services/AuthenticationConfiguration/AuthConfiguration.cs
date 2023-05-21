using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace PractiFly.WebApi.Services.AuthenticationOptions;

public class AuthConfiguration : ConfigurationAbstraction, IAuthConfiguration
{
    private const string IssuerKey = "Issuer";
    private const string AudienceKey = "Audience";
    private const string SecretKey = "Key";
    private const string TimeLifeKey = "TimeLife";

    public AuthConfiguration(IConfiguration configuration) : base(configuration)
    {
    }

    protected override string SectionName => "Secret";

    public string Issuer => GetValue(IssuerKey);
    public string Audience => GetValue(AudienceKey);
    public string Secret => GetValue(SecretKey);
    public int TimeLife => int.Parse(GetValue(TimeLifeKey));

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
}