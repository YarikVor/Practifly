using System.ComponentModel.DataAnnotations;
using PractiFly.DbEntities;

namespace PractiFly.WebApi.Dto.CourseThemes;

public class ThemeEditDto
{
    public int Id { get; set; }

    [MaxLength(EntitiesConstantLengths.Name)]
    [Required]
    public string Name { get; set; } = null!;

    public int Number { get; set; }
    public int LevelId { get; set; }

    [MaxLength(EntitiesConstantLengths.Note)]
    public string? Note { get; set; }

    [MaxLength(EntitiesConstantLengths.Description)]
    public string? Description { get; set; }
}