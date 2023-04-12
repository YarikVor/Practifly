using System.ComponentModel.DataAnnotations;

namespace PractiFly.WebApi.Dto.Level;

public class LevelCreateDto
{
    [Required]
    [MaxLength(256)]
    public string LevelName { get; set; } = null!;

    [MaxLength(65536)]
    public string? Description { get; set; }
}