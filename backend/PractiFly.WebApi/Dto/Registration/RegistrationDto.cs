using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using PractiFly.WebApi.Attributes;

namespace PractiFly.WebApi.Dto.Registration;

public class RegistrationDto
{
    [Required] [MaxLength(64)] public string UserName { get; set; } = null!;

    [Required] [MaxLength(128)] public string FirstName { get; set; } = null!;

    [Required] [MaxLength(128)] public string LastName { get; set; } = null!;

    [Required]
    [EmailAddress]
    [MaxLength(64)]
    public string Email { get; set; } = null!;

    [Required] [Phone] [MaxLength(32)] public string PhoneNumber { get; set; } = null!;

    [Required] [TodayDateConstraint] public DateOnly Birthday { get; set; }


    [PasswordPropertyText]
    [MinLength(8)]
    [MaxLength(256)]
    [Required]
    public string Password { get; set; } = null!;
}