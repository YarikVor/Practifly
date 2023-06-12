using PractiFly.WebApi.Dto.Admin.UserView;

namespace PractiFly.WebApi.Dto.CourseData;

public class CourseFullInfoDto
{
    public CourseInfoDto Course { get; set; } = null!;
    public OwnerInfoDto Owner { get; set; } = null!;
    public UserFullnameItemDto[] Users { get; set; } = null!;
}