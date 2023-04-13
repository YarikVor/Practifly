using System.ComponentModel.DataAnnotations;

namespace PractiFly.WebApi.Dto.CourseData
{
    public class CourseDataTeachersDto
    {
        [Required]
        public int TeacherId { get; set; }

        public string Teacher { get; set; } = null!;
    }
}
