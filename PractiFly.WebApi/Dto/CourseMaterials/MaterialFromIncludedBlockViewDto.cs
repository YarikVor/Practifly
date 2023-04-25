namespace PractiFly.WebApi.Dto.CourseMaterials
{
    public class MaterialFromIncludedBlockViewDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsPractical { get; set; }
        public int PriorityLevel { get; set; }

    }
}
