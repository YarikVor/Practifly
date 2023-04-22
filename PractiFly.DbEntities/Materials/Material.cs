using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PractiFly.DbEntities.Materials;

[PrimaryKey("Id")]
[Table("Material")]
public class Material
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }

    [Column("Name")]
    [MaxLength(EntitiesConstantLengths.Name)]
    [Required]

    public string Name { get; set; } = null!;

    [Column("URL")]
    [MaxLength(EntitiesConstantLengths.Url)]
    [Required]
    [Url]
    public string Url { get; set; } = null!;

    [Column("IsPractical")]
    [Required]
    public bool IsPractical { get; set; }

    [Column("Note")]
    [MaxLength(EntitiesConstantLengths.Note)]
    public string? Note { get; set; }
}