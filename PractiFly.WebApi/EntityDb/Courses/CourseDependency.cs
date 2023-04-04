using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PractiFly.WebApi.EntityDb.Courses;

[Table("CourseDependency")]
public class CourseDependency
{
    [Column("CourseId")]
    [ForeignKey("CourseId")]
    [Required]
    public int CourseId { get; set; }

    public virtual Course Course { get; set; } //TODO:

    [Column("BaseCourseId")]
    [ForeignKey("BaseCourseId")]
    [Required]
    public int BaseCourseId { get; set; }

    public virtual Course BaseCourse { get; set; } //TODO:

    [Column("CourseDependencyTypeId")]
    [ForeignKey("CourseDependencyTypeId")]
    [Required]
    public int CourseDependencyTypeId { get; set; }

    public virtual CourseDependencyType CourseDependencyType { get; set; } //TODO:

    [Column("Note")]
    [MaxLength(256)]
    public string? Note { get; set; }
}
