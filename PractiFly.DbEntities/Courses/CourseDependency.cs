using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PractiFly.DbEntities.Courses;

[Table("CourseDependency")]
[PrimaryKey("Id")]
public class CourseDependency
{
    [Key]
    [Column("Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("CourseId", Order = 1)]
    public int CourseId { get; set; }
    
    [ForeignKey("CourseId")]
    public virtual Course Course { get; set; } = null!;

    [Column("BaseCourseId", Order = 2)]
    public int BaseCourseId { get; set; }
    
    [ForeignKey("BaseCourseId")]
    public virtual Course BaseCourse { get; set; } = null!;

    [Column("CourseDependencyTypeId")]
    [ForeignKey("CourseDependencyType")]
    public int CourseDependencyTypeId { get; set; }

    [ForeignKey("CourseDependencyTypeId")]
    public virtual CourseDependencyType CourseDependencyType { get; set; } = null!;

    [Column("Note")]
    [MaxLength(EntitiesConstantLengths.Note)]
    public string? Note { get; set; }
}