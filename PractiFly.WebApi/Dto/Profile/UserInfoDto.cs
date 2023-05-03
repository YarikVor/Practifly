namespace PractiFly.WebApi.Dto.Profile;

public class UserInfoDto : UserProfileInfoViewDto
{
    public int CountCompleted { get; set; }
    public int CountInProgress { get; set; }
    public float AverageGrade { get; set; }
}