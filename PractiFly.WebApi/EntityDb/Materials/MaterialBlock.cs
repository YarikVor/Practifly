using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PractiFly.WebApi.EntityDb.Materials
{
    [Table("MaterialBlock")]
    public class MaterialBlock
    {
        [Column("ParentId")]
        public int ParentId { get; set; }

        [Column("ChildId")]
        public int ChildId { get; set; }

        [Column("Number")]
        public int Number { get; set; }

        [Column("Note")]
        [MaxLength(256)]
        public string? Note { get; set; }
    }
}
