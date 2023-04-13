using System.ComponentModel.DataAnnotations;

namespace PractiFly.WebApi.Dto.CourseData
{
    public class CourseDataUsersDto
    {
        [Required]
        public int UserId { get; set; }
        public string User { get; set; } = null!;
    }
}
