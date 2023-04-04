using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace PractiFly.WebApi.EntityDb.Users;

[Table("Users")]
[PrimaryKey("Id")]
public class Users
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }

    [Column("FirstName")]
    [MaxLength(128)]
    [Required]
    [MaybeNull]
    public string FirstName { get; set; }

    [Column("LastName")]
    [MaxLength(128)]
    [Required]
    [MaybeNull]
    public string LastName { get; set; }

    [EmailAddress]
    [Column("Email")]
    [MaxLength(64)]
    [Required]
    [MaybeNull]
    public string Email { get; set; }

    [Phone]
    [Column("Phone")]
    [MaxLength(32)]
    [Required]
    [MaybeNull]
    public string Phone { get; set; }

    [Url]
    [Column("FilePhoto")]
    [MaxLength(64)]
    [Required]
    [MaybeNull]
    public string FilePhoto { get; set; }

    [Column("RegistrationDate")]
    [MaybeNull]
    public DateOnly RegistrationDate { get; set; }

    [Column("Note")]
    [MaxLength(256)]
    public string? Note { get; set; }
}