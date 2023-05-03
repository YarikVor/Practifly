using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace PractiFly.DbEntities.Users;

[Table("AspNetUsers")]
public class User : IdentityUser<int>
{
    [Column("FirstName")]
    [MaxLength(EntitiesConstantLengths.Name)]
    [Required]
    public string FirstName { get; set; } = null!;

    [Column("LastName")]
    [MaxLength(EntitiesConstantLengths.Name)]
    [Required]
    public string LastName { get; set; } = null!;

    [Url]
    [Column("FilePhoto")]
    [MaxLength(EntitiesConstantLengths.Url)]
    [Required]
    public string FilePhoto { get; set; } = null!;

    [Column("Birthday")]
    public DateOnly Birthday { get; set; }


    [Column("RegistrationDate")]
    public DateOnly RegistrationDate { get; set; }

    [Column("Note")]
    [MaxLength(EntitiesConstantLengths.Note)]
    public string? Note { get; set; }
    
    [NotMapped]
    [InverseProperty(nameof(UserRole.User))]
    public ICollection<UserRole> UserRoles { get; set; } = null!;
}