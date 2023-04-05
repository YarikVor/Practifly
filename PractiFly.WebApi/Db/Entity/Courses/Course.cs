using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PractiFly.WebApi.EntityDb.Materials;
using PractiFly.WebApi.EntityDb.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace PractiFly.WebApi.EntityDb.Courses;

[Table("Course")]
[PrimaryKey("Id")]
public class Course
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }

    [Column("Name")]
    [MaxLength(128)]
    [Required]
    [MaybeNull]
    public string Name { get; set; }

    [Column("OwnerId")]
    [ForeignKey("OwnerId")]
    [Required]
    public int OwnerId { get; set; }

    public virtual User Owner { get; set; }

    [Column("Note")]
    [MaxLength(256)]
    public string? Note { get; set; }

    [Column("Description")]
    [MaxLength(65536)]
    public string? Description { get; set; }
}