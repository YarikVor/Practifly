namespace PractiFly.WebApi.Dto.CourseDetails;

public class UserCourseInfoDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public FullThemeWithMaterialsDto[] Themes { get; set; }
}