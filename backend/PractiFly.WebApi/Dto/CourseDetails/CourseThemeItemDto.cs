namespace PractiFly.WebApi.Dto.CourseDetails;

//теми курсу (закрита тема)
public class CourseThemeItemDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public bool IsCompleted { get; set; }
}