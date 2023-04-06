using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PractiFly.WebApi.EntityDb.Materials
{
    [Table("Unit")]
    [Keyless]
    public class Unit
    {
        [Column("MaterialId")]
        public int MaterialId { get; set; }

        [ForeignKey("MaterialKey")] 
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
}
