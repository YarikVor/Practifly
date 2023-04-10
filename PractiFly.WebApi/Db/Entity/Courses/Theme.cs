using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PractiFly.WebApi.EntityDb.Materials;

namespace PractiFly.WebApi.EntityDb.Courses;

[Table("Theme")]
[PrimaryKey("Id")]
public class Theme
{
    [Key] [Column("Id")] public int Id { get; set; }

    [Column("Name")]
    [MaxLength(128)]
    [Required]
    public string Name { get; set; } = null!;

    [Column("CourseId")] public int CourseId { get; set; }

    [ForeignKey("CourseId")] public virtual Course Course { get; set; } = null!;

    [Column("LevelId")] public int LevelId { get; set; }

    [ForeignKey("LevelId")] public virtual Level Level { get; set; } = null!;

    [Column("ParentId")] public int ParentId { get; set; }

    [ForeignKey("ParentId")] public virtual Theme Parent { get; set; } = null!;

    [Column("Number")] [Required] public int Number { get; set; }

    [Column("Note")] [MaxLength(256)] public string? Note { get; set; }

    [Column("Description")]
    [MaxLength(65536)]
    public string? Description { get; set; }
}