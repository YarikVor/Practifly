using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace PractiFly.WebApi.EntityDb.Courses;

[Table("CourseDependencyType")]
[PrimaryKey("Id")]
public class CourseDependencyType
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }

    [Column("Name")]
    [MaxLength(128)]
    [Required]
    [MaybeNull]
    public string Name { get; set; }

    [Column("Note")]
    [MaxLength(256)]
    public string? Note { get; set; }

    [Column("Description")]
    [MaxLength(65536)]
    public string? Description { get; set; }
}
