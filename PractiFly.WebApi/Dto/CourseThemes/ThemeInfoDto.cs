namespace PractiFly.WebApi.Dto.CourseThemes;

public class ThemeInfoDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int CourseId { get; set; }
    public int LevelId { get; set; }
    public int Number { get; set; }
    public string? Note { get; set; }
    public string? Description { get; set; }
}