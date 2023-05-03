namespace PractiFly.WebApi.Dto.Admin.UserView;

public class UserProfileForAdminViewDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Email { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public DateOnly Birthday { get; set; }

    public DateOnly RegistrationDate { get; set; }

    public string FilePhoto { get; set; } = null!;

    public string Role { get; set; } = null!;
}