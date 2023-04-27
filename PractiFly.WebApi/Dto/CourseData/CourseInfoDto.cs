using System.ComponentModel.DataAnnotations;

namespace PractiFly.WebApi.Dto.CourseData
{
    public class CourseInfoDto
    {
        public int Id { get; set; }
        public string Language { get; set; } = null!;

        public string CourseName { get; set; } = null!;

        public string? Note { get; set; }

        public string? Description { get; set; }
    }
}
