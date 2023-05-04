using System.ComponentModel.DataAnnotations;
using PractiFly.DbEntities;

namespace PractiFly.WebApi.Dto.CourseData;

public class CreateCourseDto
{
    [Required]
    [StringLength(2, MinimumLength = 2)]
    public string Language { get; set; } = null!;

    [Required]
    [MaxLength(EntitiesConstantLengths.Name)]
    public string Name { get; set; } = null!;

    public int OwnerId { get; set; }

    [MaxLength(EntitiesConstantLengths.Note)]
    public string? Note { get; set; }

    [MaxLength(EntitiesConstantLengths.Description)]
    public string? Description { get; set; }
}