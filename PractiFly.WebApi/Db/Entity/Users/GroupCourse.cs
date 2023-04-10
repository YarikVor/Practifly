using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PractiFly.WebApi.EntityDb.Courses;
using PractiFly.WebApi.EntityDb.Materials;

namespace PractiFly.WebApi.EntityDb.Users;

[Table("GroupCourse")]
[PrimaryKey("Id")]
public class GroupCourse
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }

    [Column("GroupId")]
    public int GroupId { get; set; }

    [ForeignKey("GroupId")]
    public virtual Group Group { get; set; } = null!;

    [Column("CourseId")]
    public int CourseId { get; set; }

    [ForeignKey("CourseId")]
    public virtual Course Course { get; set; } = null!;

    [Column("LevelId")]
    public int LevelId { get; set; }

    [ForeignKey("LevelId")]
    public virtual Level Level { get; set; } = null!;

    [Column("IsCompleted")]
    public bool IsCompleted { get; set; }

    [Column("Note")]
    [MaxLength(256)]
    public string? Note { get; set; }
}