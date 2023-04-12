using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PractiFly.DbEntities.Materials;

[PrimaryKey("Id")]
[Table("Material")]
public class Material
{
    [Column("LanguageCode")] public int LanguageId;

    [Key]
    [Column("Id")]
    public int Id { get; set; }

    [Column("Name")]
    [MaxLength(128)]
    [Required]

    public string Name { get; set; } = null!;

    [ForeignKey("LanguageId")]
    public virtual Language Language { get; set; } = null!;

    [Column("URL")]
    [MaxLength(2048)]
    [Required]
    [Url]
    public string Url { get; set; } = null!;

    [Column("IsPractical")]
    [Required]
    public bool IsPractical { get; set; }

    [Column("Note")]
    [MaxLength(256)]
    public string? Note { get; set; }
}