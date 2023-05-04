using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace PractiFly.DbEntities.Users;

[Table("AspNetRoles")]
public sealed class Role : IdentityRole<int>
{
}