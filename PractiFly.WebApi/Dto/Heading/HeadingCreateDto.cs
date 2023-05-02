using System.ComponentModel.DataAnnotations;
using PractiFly.DbEntities;

namespace PractiFly.WebApi.Dto.Heading;

public class HeadingCreateDto
{
    [Required]
    [MaxLength(128)]
    public string Name { get; set; } = null!;

    [Required]
    [MaxLength(16)]
    [RegularExpression(EntitiesConstants.HeadingRegex)]
    public string Code { get; set; } = null!;

    [Required]
    [MaxLength(16)]
    public string Udc { get; set; } = null!;

    [MaxLength(256)]
    public string? Note { get; set; }

    [MaxLength(65536)]
    public string? Description { get; set; }
}