using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PractiFly.WebApi.EntityDb.Courses;
using PractiFly.WebApi.EntityDb.Materials;

namespace PractiFly.WebApi.EntityDb.Users;

[Table("UserCourse")]
[PrimaryKey("Id")]
public class UserCourse
{
    [Key] [Column("Id")] public int Id { get; set; }

    [Column("UserId")] public int UserId { get; set; }

    [ForeignKey("UserId")] public virtual User User { get; set; } = null!;

    [Column("CourseId")] public int CourseId { get; set; }

    [ForeignKey("CourseId")] public virtual Course Course { get; set; } = null!;

    [Column("LevelId")] public int LevelId { get; set; }

    [ForeignKey("LevelId")] public virtual Level Level { get; set; } = null!;

    [Column("IsCompleted")] public bool IsCompleted { get; set; }

    [Column("LastTime")] public DateTime LastTime { get; set; }

    [Column("LastThemeId")] public int LastThemeId { get; set; }

    [ForeignKey("LastThemeId")] public virtual Theme LastTheme { get; set; } = null!;

    [Column("Grade")]
    //TODO: можливо не обов'язкове поле
    public int Grade { get; set; }

    [MaxLength(256)] [Column("Note")] public string? Note { get; set; }
}