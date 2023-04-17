using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PractiFly.DbEntities.Courses;

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
    public string Name { get; set; } = null!;

    [Column("Url")]
    [MaxLength(2048)]
    [Url]
    public string Url { get; set; }

    [Column("Description")]
    [MaxLength(65536)]
    public string? Description { get; set; }
}