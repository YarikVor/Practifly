namespace PractiFly.WebApi.Dto.CourseThemes;

public class ThemeMaterialInfoDto
{

    public int Id { get; set; }

    public int ThemeId { get; set; }
    public int MaterialId { get; set; }

    public int Number { get; set; }
    
    public bool IsBasic { get; set; }

    public int LevelId { get; set; }

    public string? Note { get; set; }

    public string? Description { get; set; }
}