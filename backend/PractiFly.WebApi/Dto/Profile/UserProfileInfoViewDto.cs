namespace PractiFly.WebApi.Dto.Profile;

public class UserProfileInfoViewDto
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }

    public DateOnly Birthday { get; set; }

    public string? FilePhoto { get; set; }

    public DateOnly RegistrationDate { get; set; }
}