using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PractiFly.WebApi.EntityDb.Materials;

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
    [MaxLength(256)]
    public string Name { get; set; } = null!;

    [Column("UDC")]
    [Required]
    [MaxLength(16)]
    public string Udc { get; set; } = null!;

    [Column("Note")]
    [MaxLength(256)]
    public string? Note { get; set; }

    [Column("Description")]
    [MaxLength(65536)]
    public string? Description { get; set; }
}