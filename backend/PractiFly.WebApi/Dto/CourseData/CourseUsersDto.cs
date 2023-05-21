namespace PractiFly.WebApi.Dto.CourseData;

public class CourseUsersDto
{
    public int UserId { get; set; }
    public int CourseId { get; set; }

    public int LevelId { get; set; }

    //public int LastThemeId { get; set; }
    public string? Note { get; set; }
}