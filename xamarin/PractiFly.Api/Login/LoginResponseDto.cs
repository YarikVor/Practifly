namespace PractiFly.Api.Login
{
    public class LoginResponseDto
    {
        public UserInfoDto User { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}
