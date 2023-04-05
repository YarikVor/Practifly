using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PractiFly.WebApi.EntityDb.Materials
{
    [Table("MaterialCompetency")]
    [Keyless]
    public class MaterialCompetency
    {
        [Column("MaterialId")]
        public int MaterialId { get; set; }

        [ForeignKey("MaterialId")]
        public virtual Material Material { get; set; } = null!;

        [Column("CompetencyId")]
        public int CompetencyId { get; set; }

        [ForeignKey("CompetencyId")]
        public virtual Competency Competency { get; set; } = null!;

        [Column("Note")]
        [MaxLength(256)]
        public string? Note { get; set; }
    }
}
