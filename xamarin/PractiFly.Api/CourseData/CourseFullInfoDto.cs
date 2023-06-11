using PractiFly.Api.Admin;

namespace PractiFly.Api.CourseData
{
    public class CourseFullInfoDto
    {
        public CourseInfoDto Course { get; set; } = null!;
        public OwnerInfoDto Owner { get; set; } = null!;
        public UserItemInfoDto[] Users { get; set; } = null!;
    }
}
