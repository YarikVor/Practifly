namespace PractiFly.WebApi.Dto.MaterialBlocks;

public class MaterialsHeadingItemDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    //public bool IsIncluded { get; set; }
    public int Priority { get; set; }
    public bool IsPractical { get; set; }
}