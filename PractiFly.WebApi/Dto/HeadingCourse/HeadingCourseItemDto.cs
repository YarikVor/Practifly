namespace PractiFly.WebApi.Dto.HeadingCourse
{
    public class HeadingCourseItemDto
    {
        public int HeadingId { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public bool IsIncluded { get; set; }
    }
}
