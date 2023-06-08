using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PractiFly.DbEntities.Materials;

namespace PractiFly.DbEntities.Courses;

[Table("Theme")]
[PrimaryKey("Id")]
public class Theme
{
    [Key]
    [Column("Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("Name")]
    [MaxLength(EntitiesConstantLengths.Name)]
    [Required]
    public string Name { get; set; } = null!;

    [Column("CourseId")]
    public int CourseId { get; set; }

    [ForeignKey("CourseId")]
    public virtual Course Course { get; set; } = null!;

    [Column("LevelId")]
    public int LevelId { get; set; }

    [ForeignKey("LevelId")]
    public virtual Level Level { get; set; } = null!;

    /*
    [Column("ParentId")]
    public int? ParentId { get; set; }

    [ForeignKey("ParentId")]
    public virtual Theme? Parent { get; set; } = null!;*/


    [Column("Number")]
    [Required]
    public int Number { get; set; }

    [Column("Note")]
    [MaxLength(EntitiesConstantLengths.Note)]
    public string? Note { get; set; }

    [Column("Description")]
    [MaxLength(EntitiesConstantLengths.Description)]
    public string? Description { get; set; }
}