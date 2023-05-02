using System.ComponentModel.DataAnnotations;
using PractiFly.DbEntities;

namespace PractiFly.WebApi.Dto.MaterialBlocks;

public class CreateMaterialDto
{
    [Required]
    [MaxLength(EntitiesConstantLengths.Name)]
    public string Name { get; set; } = null!;

    // TODO:
    public int Priority { get; set; }
        
    [MaxLength(EntitiesConstantLengths.Note)]
    public string? Note { get; set; }
    
    [Url]
    [MaxLength(EntitiesConstantLengths.Url)]
    [Required]
    public string Url { get; set; } = null!;
        
    public bool IsPractical { get; set; }

}