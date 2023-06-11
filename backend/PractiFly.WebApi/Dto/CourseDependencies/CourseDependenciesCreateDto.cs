using System.ComponentModel.DataAnnotations;

namespace PractiFly.WebApi.Dto.CourseDependencies;

public class CourseDependenciesCreateDto
{
    [Required] [MaxLength(128)] public string CourseName { get; set; } = null!;

    [MaxLength(65536)] public string? Description { get; set; }

    [Url] [MaxLength(2048)] [Required] public string DependencyFlag { get; set; } = null!;
}