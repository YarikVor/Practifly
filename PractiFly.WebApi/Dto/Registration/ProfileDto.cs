using System.ComponentModel.DataAnnotations;

namespace PractiFly.WebApi.Dto.Registration;

public class ProfileDto
{
    [Phone]
    [MaxLength(32)]
    public string? Phone { get; set; }

    [EmailAddress]
    [MaxLength(64)]
    public string? Email { get; set; }

    [Url]
    [MaxLength(2048)]
    public string? FilePhoto { get; set; }
}