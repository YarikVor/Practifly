using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace PractiFly.WebApi.EntityDb.Users
{
    [Table("Group")]
    [PrimaryKey("Id")]
    public class Group
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Name")]
        [MaxLength(256)]
        [Required]
        [MaybeNull]
        public string Name { get; set; }

        [Column("FoundationDate")]
        public DateOnly FoundationDate { get; set; }

        [Column("TerminationDate")]
        public DateOnly TerminationDate { get; set; }

        [Column("Note")]
        [MaxLength(256)]
        public string? Note { get; set; }
    }
}
