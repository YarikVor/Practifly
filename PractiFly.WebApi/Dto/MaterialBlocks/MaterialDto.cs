namespace PractiFly.WebApi.Dto.MaterialBlocks;

public class MaterialDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Url { get; set; } = null!;

    public bool IsPractical { get; set; }
        
    public string? Note { get; set; }
}