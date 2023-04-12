using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace PractiFly.DbEntities.Users;

[Table("ApplicationUser")]
[PrimaryKey("Id")]
public class ApplicationUser : IdentityUser<int>
{
    [Key]
    [Column("Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override int Id { get; set; }

    [Column("FirstName")]
    [MaxLength(128)]
    [Required]

    public override string UserName { get; set; } = null!;

    [Column("LastName")]
    [MaxLength(128)]
    [Required]
    public string LastName { get; set; } = null!;

    [EmailAddress]
    [Column("Email")]
    [MaxLength(64)]
    [Required]
    public override string Email { get; set; } = null!;

    [Phone]
    [Column("Phone")]
    [MaxLength(32)]
    [Required]
    public override string PhoneNumber { get; set; } = null!;

    [Url]
    [Column("FilePhoto")]
    [MaxLength(2048)]
    [Required]
    public string FilePhoto { get; set; } = null!;

    [Column("Birthday")]
    public DateOnly Birthday { get; set; }

    [Column("PasswordHash")]
    [MaxLength(256)]
    [Required]
    public override string PasswordHash { get; set; } = null!;

    [Column("RegistrationDate")]
    public DateOnly RegistrationDate { get; set; }

    [Column("Note")]
    [MaxLength(256)]
    public string? Note { get; set; }

    [NotMapped]
    public override string NormalizedUserName { get; set; } = null!;
}