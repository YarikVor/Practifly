using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PractiFly.WebApi.EntityDb.Materials
{
    [Table("MaterialCompetency")]
    public class MaterialCompetency
    {
        [Column("MaterialId")]
        public int MaterialId { get; set; }

        [Column("CompetencyId")]
        public int CompetencyId { get; set; }

        [Column("Note")]
        [MaxLength(256)]
        public string? Note { get; set; }
    }
}
