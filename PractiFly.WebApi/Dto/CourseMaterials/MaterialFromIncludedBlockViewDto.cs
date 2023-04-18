namespace PractiFly.WebApi.Dto.CourseMaterials
{
    public class MaterialFromIncludedBlockViewDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsPractic { get; set; }
        public int Priority { get; set; }

    }
}
