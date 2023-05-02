using System.ComponentModel.DataAnnotations;
using PractiFly.DbEntities;

namespace PractiFly.WebApi.Dto.CourseThemes;

public class ThemeMaterialEditDto
{
    public int MaterialId { get; set; }
    public int ThemeId { get; set; }

    public int Number { get; set; }
    
    public bool IsBasic { get; set; }

    public int LevelId { get; set; }
    
    [MaxLength(EntitiesConstantLengths.Note)]
    public string? Note { get; set; }
    
    [MaxLength(65536)]
    public string? Description { get; set; }
}
