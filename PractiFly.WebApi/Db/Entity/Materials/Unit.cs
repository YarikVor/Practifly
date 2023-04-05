using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

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
        [MaybeNull]
        public string Text { get; set; }

        [Column("URL")]
        [MaxLength(2048)]
        [Required]
        [MaybeNull]
        public string Url { get; set; }
    }
}
