using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PractiFly.WebApi.EntityDb.Materials;

[Table("Level")]
[PrimaryKey("Id")]
public class Level
{
    [Key] [Column("Id")] public int Id { get; set; }

    [Column("Name")]
    [MaxLength(256)]
    [Required]

    public string Name { get; set; } = null!;

    [Column("Number")] [Required] public int Number { get; set; }

    [Column("Note")] [MaxLength(256)] public string? Note { get; set; }

    [Column("Description")]
    [MaxLength(65536)]
    public string? Description { get; set; }
}