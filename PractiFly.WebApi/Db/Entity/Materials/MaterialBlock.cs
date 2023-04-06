using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PractiFly.WebApi.EntityDb.Materials
{
    [Table("MaterialBlock")]
    [Keyless]
    public class MaterialBlock
    {
        [Column("ParentId")]
        public int ParentId { get; set; }

        [ForeignKey("ParentId")]
        public virtual Material Parent { get; set; } = null!;

        [Column("ChildId")]
        public int ChildId { get; set; }

        [ForeignKey("ChildId")]
        public virtual Material Child { get; set; } = null!;

        [Column("Number")]
        [Required]
        public int Number { get; set; }

        [Column("Note")]
        [MaxLength(256)]
        public string? Note { get; set; }
    }
}
