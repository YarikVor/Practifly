using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PractiFly.DbEntities.Materials;

[Table("Heading")]
[PrimaryKey("Id")]
public class Heading
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }

    [Column("Code")]
    [Required]
    [MaxLength(16)]
    public string Code { get; set; } = null!;

    [Column("Name")]
    [Required]
    [MaxLength(EntitiesConstantLengths.Name)]
    public string Name { get; set; } = null!;

    [Column("UDC")]
    [Required]
    [MaxLength(EntitiesConstantLengths.Udc)]
    public string Udc { get; set; } = null!;

    [Column("Note")]
    [MaxLength(EntitiesConstantLengths.Note)]
    public string? Note { get; set; }

    [Column("Description")]
    [MaxLength(EntitiesConstantLengths.Description)]
    public string? Description { get; set; }
}