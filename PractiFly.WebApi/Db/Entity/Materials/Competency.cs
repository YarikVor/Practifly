using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PractiFly.WebApi.EntityDb.Materials;

[Table("Competency")]
[PrimaryKey("Id")]
public class Competency
{
    [Key] [Column("Id")] public int Id { get; set; }

    [Column("Name")]
    [MaxLength(128)]
    [Required]
    public string Name { get; set; } = null!;

    [Column("HeadingId")] public int HeadingId { get; set; }

    [ForeignKey("HeadingId")] public virtual Heading Heading { get; set; } = null!;

    [Column("ParentId")] public int? ParentId { get; set; }

    [ForeignKey("ParentId")] public virtual Competency? Parent { get; set; }

    [Column("Note")] [MaxLength(256)] public string? Note { get; set; }

    [Column("Description")]
    [MaxLength(65536)]
    public string? Description { get; set; }
}