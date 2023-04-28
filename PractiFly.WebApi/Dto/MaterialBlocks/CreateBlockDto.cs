namespace PractiFly.WebApi.Dto.MaterialBlocks
{
    public class CreateBlockDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Language { get; set; } = null!;
        public int Priority { get; set; }
        public string? Note { get; set; }
        public string Url { get; set; } = null!;
        public bool IsPractical { get; set; }

    }
}
