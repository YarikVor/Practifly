using System.ComponentModel.DataAnnotations;

namespace PractiFly.WebApi.Dto.CourseThemes
{
    public class ThemeEditDto
    {
        [Required]
        [MaxLength(128)]
        public string Name { get; set; } = null!;

        [MaxLength(256)]
        public string? Note { get; set; }
    }
}
