using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PractiFly.WebApi.EntityDb.Materials;

[Table("Language")]
[PrimaryKey("Id")]
public class Language
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }

    [Column("Code")]
    [MaxLength(2)]
    [Required]
    public string Code { get; set; } = null!;

    [Column("Name")]
    [MaxLength(128)]
    [Required]
    public string Name { get; set; } = null!;

    [Column("OriginalName")]
    [MaxLength(128)]
    [Required]
    public string OriginalName { get; set; } = null!;

    [Column("Note")]
    [MaxLength(256)]
    public string? Note { get; set; }
}