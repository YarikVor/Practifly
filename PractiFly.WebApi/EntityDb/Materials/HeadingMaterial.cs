using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PractiFly.WebApi.EntityDb.Materials
{
    [Table("HeadingMaterial")]
    public class HeadingMaterial
    {
        [Column("HeadingId")]
        public int HeadingId { get; set; }

        [Column("MaterialId")]
        public int MaterialId { get; set; }

        [Column("Note")]
        [MaxLength(256)]
        public string? Note { get; set; }
    }
}
