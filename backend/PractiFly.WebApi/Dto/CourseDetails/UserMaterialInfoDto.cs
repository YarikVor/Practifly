namespace PractiFly.WebApi.Dto.CourseDetails;

public class UserMaterialInfoDto
{
    public int MaterialId { get; set; }
    public int? Grade { get; set; }
    public string ResultUrl { get; set; } = null!;
    public bool IsCompleted { get; set; }
}