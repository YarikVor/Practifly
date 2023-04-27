namespace PractiFly.WebApi.Dto.Heading;

public class HeadingItemInCourseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public bool IsIncluded { get; set; }
}