using PractiFly.WebApi.Dto.Profile;

namespace PractiFly.WebApi.Dto.Registration;

public class UserTokenInfoDto
{
    public UserProfileInfoViewDto User { get; set; } = null!;

    public string Token { get; set; } = null!;
}