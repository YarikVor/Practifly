using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PractiFly.WebApi.EntityDb.Materials;

namespace PractiFly.WebApi.EntityDb.Courses;

[Table("ThemeMaterial")]
[PrimaryKey("Id")]
public class ThemeMaterial
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }

    [Column("ThemeId")]
    public int ThemeId { get; set; }

    [ForeignKey("ThemeId")]
    public virtual Theme Theme { get; set; } = null!;

    [Column("MaterialId")]
    public int MaterialId { get; set; }

    [ForeignKey("MaterialId")]
    public virtual Material Material { get; set; } = null!;

    [Column("Number")]
    [Required]
    public int Number { get; set; }

    [Column("IsBasic")]
    [Required]
    public bool IsBasic { get; set; }

    [Column("LevelId")]
    public int LevelId { get; set; }

    [ForeignKey("LevelId")]
    public virtual Level Level { get; set; } = null!;

    [Column("Note")]
    [MaxLength(256)]
    public string? Note { get; set; }
}