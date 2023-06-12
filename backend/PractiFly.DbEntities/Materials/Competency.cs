using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PractiFly.DbEntities.Materials;

[Table("Competency")]
[PrimaryKey("Id")]
public class Competency
{
    [Key]
    [Column("Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("Name")]
    [MaxLength(EntitiesConstantLengths.Name)]
    [Required]
    public string Name { get; set; } = null!;

    [Column("HeadingId")]
    public int HeadingId { get; set; }

    [ForeignKey("HeadingId")]
    public virtual Heading Heading { get; set; } = null!;

    [Column("ParentId")]
    public int? ParentId { get; set; }

    [ForeignKey("ParentId")]
    public virtual Competency? Parent { get; set; }

    [Column("Note")]
    [MaxLength(EntitiesConstantLengths.Note)]
    public string? Note { get; set; }

    [Column("Description")]
    [MaxLength(EntitiesConstantLengths.Description)]
    public string? Description { get; set; }
}