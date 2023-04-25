namespace PractiFly.WebApi.Dto.CourseMaterials
{
    public abstract class MaterialBlocksDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsPractical { get; set; }
        public bool IsIncluded { get; set; }
        public int PriorityLevel { get; set; }
        public abstract string Type { get; } 
    }

    public class MaterialItemDto : MaterialBlocksDto
    {
        public override string Type => "material";
    }

    public class BlockItemDto : MaterialBlocksDto
    {
        public override string Type => "block";
    }

}
