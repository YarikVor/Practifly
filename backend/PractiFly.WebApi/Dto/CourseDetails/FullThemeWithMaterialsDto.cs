namespace PractiFly.WebApi.Dto.CourseDetails;

public class FullThemeWithMaterialsDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public bool IsCompleted { get; set; }

    public int Grade { get; set; }

    public CourseMaterialItemDto[] Materials { get; set; } = null!;
}