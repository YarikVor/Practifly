using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PractiFly.WebApi.EntityDb.Materials
{
    [Table("HeadingCompetency")]
    [PrimaryKey("Id")]
    public class HeadingCompetency
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("CompetencyId")]
        public int CompetencyId { get; set; }

        [Column("LevelId")]
        public int LevelId { get; set; }

        [Column("Note")]
        [MaxLength(256)]
        public string? Note { get; set; }
    }
}
