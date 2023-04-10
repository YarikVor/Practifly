using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PractiFly.WebApi.EntityDb.Materials;

namespace PractiFly.WebApi.EntityDb.Courses;

[Table("CourseHeading")]
[PrimaryKey("Id")]
public class CourseHeading
{
    [Key] [Column("Id")] public int Id { get; set; }

    [Column("CourseId")] public int CourseId { get; set; }

    [ForeignKey("CourseId")] public virtual Course Course { get; set; } = null!;

    [Column("HeadingId")] public int HeadingId { get; set; }

    [ForeignKey("HeadingId")] public virtual Heading Heading { get; set; } = null!;

    [Column("IsBasic")] [Required] public bool IsBasic { get; set; }

    [Column("Note")] [MaxLength(256)] public string? Note { get; set; }
}