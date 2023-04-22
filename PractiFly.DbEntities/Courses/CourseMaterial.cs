using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PractiFly.DbEntities.Materials;

namespace PractiFly.DbEntities.Courses;

[Table("CourseMaterial")]
[PrimaryKey("Id")]
public class CourseMaterial
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }

    [Column("CourseId")]
    public int CourseId { get; set; }

    [ForeignKey("CourseId")]
    public virtual Course Course { get; set; } = null!;

    [Column("MaterialId")]
    public int MaterialId { get; set; }

    [ForeignKey("MaterialId")]
    public virtual Material Material { get; set; } = null!;

    [Column("PriorityLevel")]
    [Required]
    public int PriorityLevel { get; set; }

    [Column("IsReserved")]
    [Required]
    public bool IsReserved { get; set; }

    [Column("Note")]
    [MaxLength(EntitiesConstantLengths.Note)]
    public string? Note { get; set; }
}