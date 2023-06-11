using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PractiFly.DbEntities.Materials;

[Table("Language")]
[PrimaryKey("Id")]
public class Language
{
    [Key]
    [Column("Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("Code")]
    [MaxLength(EntitiesConstantLengths.Code)]
    [Required]
    public string Code { get; set; } = null!;

    [Column("Name")]
    [MaxLength(EntitiesConstantLengths.Name)]
    [Required]
    public string Name { get; set; } = null!;

    [Column("OriginalName")]
    [MaxLength(EntitiesConstantLengths.Name)]
    [Required]
    public string OriginalName { get; set; } = null!;

    [Column("Note")]
    [MaxLength(EntitiesConstantLengths.Note)]
    public string? Note { get; set; }
}