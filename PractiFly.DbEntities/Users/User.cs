using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace PractiFly.DbEntities.Users;

public sealed class User: IdentityUser<int>
{
    [Column("FirstName")]
    [MaxLength(128)]
    [Required]
    public string FirstName { get; set; } = null!;

    [Column("LastName")]
    [MaxLength(128)]
    [Required]
    public string LastName { get; set; } = null!;

    [Url]
    [Column("FilePhoto")]
    [MaxLength(2048)]
    [Required]
    public string FilePhoto { get; set; } = null!;

    [Column("Birthday")]
    public DateOnly Birthday { get; set; }


    [Column("RegistrationDate")]
    public DateOnly RegistrationDate { get; set; }

    [Column("Note")]
    [MaxLength(256)]
    public string? Note { get; set; }
}