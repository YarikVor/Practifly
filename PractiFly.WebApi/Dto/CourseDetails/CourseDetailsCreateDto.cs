//Сторінка CourseDetails в Figma

using System.ComponentModel.DataAnnotations;

namespace PractiFly.WebApi.Dto.CourseDetails
{
    //інфо, створення
    public class CourseDetailsCreateDto
    {
        [Required]
        [MaxLength(256)]
        public string Name { get; set; } = null!;

        [MaxLength(65536)]
        public string? Description { get; set; }
    }
}
