using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PractiFly.DbEntities.Courses;
using PractiFly.DbEntities.Materials;

namespace PractiFly.DbEntities.Users;

[Table("UserTheme")]
[PrimaryKey("Id")]
public class UserTheme
{
    [Key]
    [Column("Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("UserId")]
    public int UserId { get; set; }

    [ForeignKey("UserId")]
    public virtual User User { get; set; } = null!;

    [Column("ThemeId")]
    public int ThemeId { get; set; }

    [ForeignKey("ThemeId")]
    public virtual Theme Theme { get; set; } = null!;

    [Column("IsCompleted")]
    [Required]
    public bool IsCompleted { get; set; }

    [Column("LevelId")]
    public int LevelId { get; set; }

    [ForeignKey("LevelId")]
    public virtual Level Level { get; set; } = null!;

    [Column("Grade")]
    public int? Grade { get; set; }

    [Column("Note")]
    [MaxLength(EntitiesConstantLengths.Note)]
    public string? Note { get; set; }
}