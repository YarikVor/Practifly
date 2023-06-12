using System.ComponentModel.DataAnnotations;
using PractiFly.WebApi.Attributes;

namespace PractiFly.WebApi.Dto.Profile;

public class UserProfileInfoEditDto
{
    [Required] [MaxLength(128)] public string FirstName { get; set; } = null!;

    [Required] [MaxLength(128)] public string LastName { get; set; } = null!;

    [Phone] [MaxLength(32)] public string? PhoneNumber { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength(64)]
    public string? Email { get; set; }

    [TodayDateConstraint] public DateOnly Birthday { get; set; }
}