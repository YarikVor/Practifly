namespace PractiFly.WebApi.Dto.CourseDependencies
{
    public class CourseDependenciesViewDto
    {
        public string CourseName { get; set; } = null!;
        public string? Description { get; set; }
        public string DependencyFlag { get; set; } = null!;
    }
}
