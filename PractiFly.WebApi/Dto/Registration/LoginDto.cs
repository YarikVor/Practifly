using System.ComponentModel.DataAnnotations;

namespace PractiFly.WebApi.Controllers;

public class LoginDto
{
    [Required]
    [EmailAddress]
    [MaxLength(64)]
    public string Email { get; set; } = null!;

    [Required]
    [MinLength(8)]
    [MaxLength(256)]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}