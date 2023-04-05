using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace PractiFly.WebApi.EntityDb.Materials
{
    [Table("Unit")]
    public class Unit
    {
        [Column("MaterialId")]
        public int MaterialId { get; set; }

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
