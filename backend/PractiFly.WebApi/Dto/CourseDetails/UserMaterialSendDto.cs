namespace PractiFly.WebApi.Dto.CourseDetails;

public class UserMaterialSendDto
{
    public int MaterialId { get; set; }

    public string? ResultUrl { get; set; }

    public bool IsCompleted { get; set; }
}