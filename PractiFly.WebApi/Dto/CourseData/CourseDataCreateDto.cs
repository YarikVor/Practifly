using System.ComponentModel.DataAnnotations;

namespace PractiFly.WebApi.Dto.CourseData
{
    public class CourseDataCreateDto
    {
        [Required]
        public string CourseName { get; set; } = null!;

        [MaxLength(256)]
        public string? Note { get; set; }

        [MaxLength(65536)]
        public string? Description { get; set; }
    }
}
