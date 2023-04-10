using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PractiFly.WebApi.EntityDb.Materials;

[Table("Unit")]
[PrimaryKey("Id")]
public class Unit
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }

    [Column("MaterialId")]
    public int MaterialId { get; set; }

    [ForeignKey("MaterialId")]
    public virtual Material Material { get; set; } = null!;

    [Column("Text")]
    [Required]
    [MaxLength(2048)]
    public string Text { get; set; } = null!;

    [Column("URL")]
    [MaxLength(2048)]
    [Required]
    [Url]
    public string Url { get; set; } = null!;
}