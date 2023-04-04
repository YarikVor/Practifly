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
        [MaybeNull]
        public string Code { get; set; }

        [Column("Name")]
        [MaxLength(128)]
        [MaybeNull]
        public string Name { get; set; }

        [Column("OriginalName")]
        [MaxLength(128)]
        [MaybeNull]
        public string OriginalName { get; set; }

        [Column("Note")]
        [MaxLength(256)]
        public string? Note { get; set; }
    }
}
