namespace PractiFly.WebApi.Dto.CourseData;

public class CourseInfoDto
{
    public int Id { get; set; }
    public string Language { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Note { get; set; }
    public string? Description { get; set; }
}