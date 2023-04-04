using PractiFly.WebApi.EntityDb.Materials;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PractiFly.WebApi.EntityDb.Courses
{
    [Table("Theme")]
    public class ThemeMaterial
    {
        [Column("ThemeId")]
        [ForeignKey("ThemeId")]
        [Required]
        public int ThemeId { get; set; }
        public virtual Theme Theme { get; set; } //TODO:

        [Column("MaterialId")]
        [ForeignKey("MaterialId")]
        [Required]
        public int MaterialId { get; set; }
        public virtual Material Material { get; set; } //TODO:

        [Column("Number")]
        [ForeignKey("Number")]
        [Required]
        public int Number { get; set; }

        [Column("IsBasic")]
        [ForeignKey("IsBasic")]
        [Required]
        public bool IsBasic { get; set; }

        [Column("LevelId")]
        [ForeignKey("LevelId")]
        [Required]
        public int LevelId { get; set; }
        public virtual Level Level { get; set; } //TODO:

        [Column("Note")]
        [MaxLength(256)]
        public string? Note { get; set; }
    }
}
