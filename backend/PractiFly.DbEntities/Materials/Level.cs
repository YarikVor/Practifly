using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PractiFly.DbEntities.Materials;

[Table("Level")]
[PrimaryKey("Id")]
public class Level
{
    [Key]
    [Column("Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("Name")]
    [MaxLength(EntitiesConstantLengths.Name)]
    [Required]

    public string Name { get; set; } = null!;

    [Column("Number")]
    [Required]
    public int Number { get; set; }

    [Column("Note")]
    [MaxLength(EntitiesConstantLengths.Note)]
    public string? Note { get; set; }

    [Column("Description")]
    [MaxLength(EntitiesConstantLengths.Description)]
    public string? Description { get; set; }
}