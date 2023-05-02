using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PractiFly.DbEntities;
using PractiFly.DbEntities.Courses;

namespace PractiFly.WebApi.Dto.CourseThemes;

public class ThemeCreateDto
{
    [MaxLength(EntitiesConstantLengths.Name)]
    [Required]
    public string Name { get; set; } = null!;

    public int CourseId { get; set; }
    
    public int LevelId { get; set; }

    public int Number { get; set; }
    
    [MaxLength(EntitiesConstantLengths.Note)]
    public string? Note { get; set; }
    
    [MaxLength(EntitiesConstantLengths.Description)]
    public string? Description { get; set; }
}