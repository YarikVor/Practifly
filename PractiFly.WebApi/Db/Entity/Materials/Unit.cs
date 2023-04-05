using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace PractiFly.WebApi.EntityDb.Materials
{
    [Table("Unit")]
    [Keyless]
    public class Unit
    {
        [Column("MaterialId")]
        public int MaterialId { get; set; }
        
        [ForeignKey("MaterialId")]
        public virtual Material Material { get; set; }

        [ForeignKey("MaterialKey")] 
        public virtual Material Material { get; set; } = null!;

        [Column("Text")]
        [Required]
        [MaxLength(2048)]
        public string Text { get; set; } = null!;

        [Column("URL")]
        [MaxLength(2048)]
        [Required]
        public string Url { get; set; } = null!;
    }
}
