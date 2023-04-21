namespace PractiFly.WebApi.Dto.CourseThemes
{
    public class MaterialsMenuDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Grade { get; set; }
        public bool IsSelected { get; set; }

    }
}
