using PractiFly.WebApi.Dto.Admin.UserView;

namespace PractiFly.WebApi.Dto.CourseData;

public class CourseFullInfoDto
{
    public CourseInfoDto CourseInfoDto { get; set; } = null!;
    public OwnerInfoDto OwnerInfoDto { get; set; } = null!;
    public UserFullnameItemDto[] UserFullnameItemDto { get; set; } = null!;
}