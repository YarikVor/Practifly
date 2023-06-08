using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PractiFly.DbEntities.Materials;

[Table("Unit")]
[PrimaryKey("Id")]
public class Unit
{
    [Key]
    [Column("Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("MaterialId")]
    public int MaterialId { get; set; }

    [ForeignKey("MaterialId")]
    public virtual Material Material { get; set; } = null!;

    [Column("Text")]
    [Required]
    [MaxLength(EntitiesConstantLengths.Text)]
    public string Text { get; set; } = null!;

    [Column("URL")]
    [MaxLength(EntitiesConstantLengths.Url)]
    [Required]
    [Url]
    public string Url { get; set; } = null!;
}