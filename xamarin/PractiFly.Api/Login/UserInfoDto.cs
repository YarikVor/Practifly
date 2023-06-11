namespace PractiFly.Api.Login
{
    public class UserInfoDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Birthday { get; set; } = null!;
        public string FilePhoto { get; set; } = null!;
        public string RegistrationDate { get; set; } = null!;
    }
}
