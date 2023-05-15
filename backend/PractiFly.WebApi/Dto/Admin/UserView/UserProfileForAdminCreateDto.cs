using System.ComponentModel.DataAnnotations;
using PractiFly.DbEntities;
using PractiFly.WebApi.Attributes;

namespace PractiFly.WebApi.Dto.Admin.UserView;

public class UserProfileForAdminCreateDto
{
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
    public string PhoneNumber { get; set; } = null!;

    [Required]
    [StringLength(128, MinimumLength = 8)]
    public string Password { get; set; } = null!;

    [Required]
    [MaxLength(EntitiesConstantLengths.Name)]
    public string UserName { get; set; } = null!;

    [Required]
    [TodayDateConstraint]
    public DateOnly Birthday { get; set; }

    [Url]
    [MaxLength(2048)]
    public string? FilePhoto { get; set; }

    [RoleString]
    public string Role { get; set; } = null!;
}