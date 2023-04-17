namespace PractiFly.WebApi.Dto.Profile
{
    public class UserProfileInfoViewDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Email { get; set; }

        public DateOnly Birthday { get; set; }

        public string? FilePhoto { get; set; }

        public DateOnly RegistrationDate { get; set; }
    }
}
