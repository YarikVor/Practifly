//Сторінка CourseDetails в Figma

using System.ComponentModel.DataAnnotations;

namespace PractiFly.WebApi.Dto.CourseDetails
{
    public class CourseDetailsCreateDto
    {
        [Required]
        [MaxLength(256)]
        public string CourseName { get; set; } = null!;

        [MaxLength(65536)]
        public string? Description { get; set; }
    }
}
