using Microsoft.EntityFrameworkCore;
using PractiFly.WebApi.EntityDb.Materials;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace PractiFly.WebApi.EntityDb.Courses
{
    [Table("Theme")]
    [PrimaryKey("Id")]
    public class Theme
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Name")]
        [MaxLength(128)]
        [Required]
        [MaybeNull]
        public string Name { get; set; }

        [Column("CourseId")]
        [ForeignKey("CourseId")]
        [Required]
        public int CourseId { get; set; }
        public virtual Course Course { get; set; } //TODO:

        [Column("LevelId")]
        [ForeignKey("LevelId")]
        [Required]
        public int LevelId { get; set; }
        public virtual Level Level { get; set; } //TODO:

        [Column("ParentId")]
        [ForeignKey("ParentId")]
        public int ParentId { get; set; }

        public virtual Theme id { get; set; } //TODO:

        [Column("Number")]
        [ForeignKey("Number")]
        [Required]
        public int Number { get; set; }

        [Column("Note")]
        [MaxLength(256)]
        public string? Note { get; set; }

        [Column("Description")]
        [MaxLength(65536)]
        public string? Description { get; set; }
    }
}
