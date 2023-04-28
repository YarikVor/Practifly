using System.ComponentModel.DataAnnotations;

namespace PractiFly.WebApi.Dto.Heading;

public class HeadingEditDto
{
    
    public int Id {get; set; }
    
    [Required]
    [MaxLength(128)]
    public string Name { get; set; } = null!;

    [Required]
    [MaxLength(16)]
    public string Code { get; set; } = null!;

    [Required]
    [RegularExpression(@"^\d{2}(?:\.\d{2}){0,3}$")]
    public string Udc { get; set; } = null!;

    [MaxLength(256)]
    public string? Note { get; set; }

    [MaxLength(65536)]
    public string? Description { get; set; }
}