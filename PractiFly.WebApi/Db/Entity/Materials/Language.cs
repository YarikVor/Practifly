using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace PractiFly.WebApi.EntityDb.Materials
{
    [Table("Language")]
    [PrimaryKey("Code")]
    public class Language
    {
        [Key]
        [Column("Code")]
        [MaxLength(2)]
        [Required]
        public string Code { get; set; } = null!;

        [Column("Name")]
        [MaxLength(128)]
        [Required]
        public string Name { get; set; } = null!;

        [Column("OriginalName")]
        [MaxLength(128)]
        [Required]
        public string OriginalName { get; set; } = null!;

        [Column("Note")]
        [MaxLength(256)]
        public string? Note { get; set; }
    }
}
