using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PractiFly.WebApi.Dto.Registration;

public class RegistrationDto
{
    [Required]
    [MaxLength(128)]
    public string Name { get; set; } = null!;

    [Required]
    [MaxLength(128)]
    public string Surname { get; set; } = null!;

    [Required]
    [EmailAddress]
    [MaxLength(64)]
    public string Email { get; set; } = null!;

    [Required]
    [Phone]
    [MaxLength(32)]
    public string Phone { get; set; } = null!;

    [Required]
    public int? DayBirth { get; set; }

    [Required]
    [Range(1, 12)]
    public int? MonthBirth { get; set; }

    [Required]
    public int? YearBirth { get; set; }

    [PasswordPropertyText]
    [MinLength(8)]
    [MaxLength(256)]
    [Required]
    public string Password { get; set; } = null!;

    [PasswordPropertyText]
    [MinLength(8)]
    [MaxLength(256)]
    [Required]
    public string PasswordConfirm { get; set; } = null!;
}