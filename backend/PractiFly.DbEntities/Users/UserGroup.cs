using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PractiFly.DbEntities.Users;

[Table("UserGroup")]
[PrimaryKey("Id")]
public class UserGroup
{
    [Key]
    [Column("Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("UserId")]
    public int UserId { get; set; }

    [ForeignKey("UserId")]
    public virtual User User { get; set; } = null!;

    [Column("GroupId")]
    public int GroupId { get; set; }

    [ForeignKey("GroupId")]
    public virtual Group Group { get; set; } = null!;

    [Column("IsActive")]
    public bool IsActive { get; set; }

    [Column("Note")]
    [MaxLength(EntitiesConstantLengths.Note)]
    public string? Note { get; set; }
}