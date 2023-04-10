using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PractiFly.WebApi.EntityDb.Users;

namespace PractiFly.WebApi.EntityDb.Courses;

[Table("Course")]
[PrimaryKey("Id")]
public class Course
{
    [Key]
    [Column("Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("Name")]
    [MaxLength(128)]
    [Required]
    public string Name { get; set; } = null!;

    [Column("OwnerId")]
    public int OwnerId { get; set; }

    [ForeignKey("OwnerId")]
    public virtual User Owner { get; set; } = null!;

    [Column("Note")]
    [MaxLength(256)]
    public string? Note { get; set; }

    [Column("Description")]
    [MaxLength(65536)]
    public string? Description { get; set; }
}