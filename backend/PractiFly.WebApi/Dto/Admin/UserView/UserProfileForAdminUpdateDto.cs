using System.ComponentModel.DataAnnotations;
using PractiFly.WebApi.Attributes;

namespace PractiFly.WebApi.Dto.Admin.UserView;

public class UserProfileForAdminUpdateDto
{
    public int Id { get; set; }

    [Required]
    [MaxLength(128)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MaxLength(128)]
    public string LastName { get; set; } = null!;

    [Required]
    [EmailAddress]
    [MaxLength(128)]
    public string Email { get; set; } = null!;

    [Phone]
    [MaxLength(32)]
    public string? Phone { get; set; }

    [Required]
    [TodayDateConstraint]
    public DateOnly Birthday { get; set; }

    //[Required]
    //[TodayDateConstraint]
    //public DateOnly RegistrationDate { get; set; }
    [Url]
    [MaxLength(2048)]
    public string? FilePhoto { get; set; }

    [RoleString]
    [Required]
    public string Role { get; set; } = null!;
}