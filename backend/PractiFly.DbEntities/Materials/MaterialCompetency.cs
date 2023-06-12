using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PractiFly.DbEntities.Materials;

[Table("MaterialCompetency")]
[PrimaryKey("Id")]
public class MaterialCompetency
{
    [Key]
    [Column("Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("MaterialId")]
    public int MaterialId { get; set; }

    [ForeignKey("MaterialId")]
    public virtual Material Material { get; set; } = null!;

    [Column("CompetencyId")]
    public int CompetencyId { get; set; }

    [ForeignKey("CompetencyId")]
    public virtual Competency Competency { get; set; } = null!;

    [Column("Note")]
    [MaxLength(EntitiesConstantLengths.Note)]
    public string? Note { get; set; }
}