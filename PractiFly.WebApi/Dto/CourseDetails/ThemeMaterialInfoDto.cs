namespace PractiFly.WebApi.Dto.CourseDetails;

public class ThemeMaterialInfoDto
{
    public MaterialDetailsViewDto Material { get; set; } = null!;
    public UserMaterialInfoDto ViewStatus { get; set; } = null!;
    public UserMaterialSendDto SendStatus { get; set; } = null!;
}