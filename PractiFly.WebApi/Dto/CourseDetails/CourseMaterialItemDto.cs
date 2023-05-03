namespace PractiFly.WebApi.Dto.CourseDetails;

//матеріали
public class CourseMaterialItemDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Grade { get; set; }

    public bool IsCompleted { get; set; }
}