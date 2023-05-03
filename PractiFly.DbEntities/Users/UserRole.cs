using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace PractiFly.DbEntities.Users;

[Table("AspNetUserRoles")]
[PrimaryKey(nameof(UserRole.RoleId), nameof(UserRole.UserId))]
public class UserRole : IdentityUserRole<int>
{
    [Key]
    [Column(Order = 0)]
    public override int UserId { get; set; }

    [Key]
    [Column(Order = 1)]
    public override int RoleId { get; set; }

    [ForeignKey(nameof(UserId))]
    [InverseProperty("UserRoles")]
    public virtual User User { get; set; } = null!;

    [ForeignKey(nameof(RoleId))]
    public virtual Role Role { get; set; } = null!;
}