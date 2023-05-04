namespace PractiFly.WebApi.Dto.CourseDetails;

//TODO: Create Dto.
public class UserMaterialSendDto
{
    public int Id { get; set; }

    public string? ResultUrl { get; set; }

    //IsPassed?
    public bool IsCompleted { get; set; }
}