using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using PractiFly.WebApi.Db.Configuration.Users;

namespace PractiFly.WebApi.EntityDb.Users;

[Table("User")]
[PrimaryKey("Id")]
[EntityTypeConfiguration(typeof(UserConfiguration))]
public class User
{
    [Key]
    [Column("Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("FirstName")]
    [MaxLength(128)]
    [Required]
    public string FirstName { get; set; } = null!;

    [Column("LastName")]
    [MaxLength(128)]
    [Required]
    public string LastName { get; set; } = null!;

    [EmailAddress]
    [Column("Email")]
    [MaxLength(64)]
    [Required]
    public string Email { get; set; } = null!;

    [Phone]
    [Column("Phone")]
    [MaxLength(32)]
    [Required]
    public string Phone { get; set; } = null!;

    [Url]
    [Column("FilePhoto")]
    [MaxLength(64)]
    [Required]
    public string FilePhoto { get; set; } = null!;

    [Column("RegistrationDate")]
    public DateOnly RegistrationDate { get; set; }

    [Column("Note")]
    [MaxLength(256)]
    public string? Note { get; set; }
}