namespace PractiFly.WebApi.Dto.CourseDetails;

//інфо, перегляд
public class MaterialDetailsViewDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string Url { get; set; } = null!;
}