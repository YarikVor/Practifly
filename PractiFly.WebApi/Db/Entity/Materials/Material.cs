using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace PractiFly.WebApi.EntityDb.Materials
{
    [PrimaryKey("Id")]
    [Table("Material")]
    public class Material
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Name")]
        [MaxLength(128)]
        [Required]
        [MaybeNull]
        public string Name { get; set; }

        [Column("LanguageCode")]
        [MaxLength(2)]
        [Required]
        [MaybeNull]
        public string LanguageCode { get; set; }

        [Column("URL")]
        [MaxLength(2048)]
        [Required]
        [MaybeNull]
        public string Url { get; set; }

        [Column("IsPractical")]
        public bool IsPractical { get; set; }

        [Column("Note")]
        [MaxLength(256)]
        public string? Note { get; set; }
    }
}
