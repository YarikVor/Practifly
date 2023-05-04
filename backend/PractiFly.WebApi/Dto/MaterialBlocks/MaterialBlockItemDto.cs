namespace PractiFly.WebApi.Dto.MaterialBlocks;

public class MaterialBlockItemDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public bool IsIncluded { get; set; }
    public bool IsPractical { get; set; }
}