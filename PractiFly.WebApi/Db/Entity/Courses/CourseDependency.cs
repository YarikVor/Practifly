using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PractiFly.WebApi.EntityDb.Courses;

[Table("CourseDependency")]
[PrimaryKey("Id")]
public class CourseDependency
{
    [Key] [Column("Id")] public int Id { get; set; }

    [Column("CourseId")] public int CourseId { get; set; }

    [ForeignKey("CourseId")] public virtual Course Course { get; set; } = null!;

    [Column("BaseCourseId")] public int BaseCourseId { get; set; }

    [ForeignKey("BaseCourseId")] public virtual Course BaseCourse { get; set; } = null!;

    [Column("CourseDependencyTypeId")] public int CourseDependencyTypeId { get; set; }

    [ForeignKey("CourseDependencyTypeId")]
    public virtual CourseDependencyType CourseDependencyType { get; set; } = null!;

    [Column("Note")] [MaxLength(256)] public string? Note { get; set; }
}