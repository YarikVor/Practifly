using System.ComponentModel.DataAnnotations;
using PractiFly.WebApi.Attributes;

namespace PractiFly.WebApi.Dto.Admin.UserView;

public class UserFilteringDto
{
    [MaxLength(128)]
    public string? FirstName { get; set; }

    [MaxLength(128)]
    public string? LastName { get; set; }

    [Phone]
    [MaxLength(32)]
    public string? Phone { get; set; }

    public DateOnly? RegistrationDateFrom { get; set; }
    public DateOnly? RegistrationDateTo { get; set; }

    [EmailAddress]
    [MaxLength(128)]
    public string? Email { get; set; }

    [RoleString]
    public string? Role { get; set; }
}