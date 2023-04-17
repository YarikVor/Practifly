using System.ComponentModel;

namespace PractiFly.WebApi.Dto.MyCourse
{
    public class UserCourseStatusDto
    {
        public int Id { get; set; }
        public string Language { get; set; } = null!;
        public string Name { get; set; } = null!;

        [Description("Count of completed courses")]
        public int CountProgress { get; set; }

        [Description("Count of all courses")]
        public int CountThemes { get; set; }
        
        [Description("Course completion status")]
        public bool IsCompleted { get; set; }
        public bool IsChecked { get; set; }

        public string? Description { get; set; }
        public float Grade { get; set; }
        public float GradeAverage { get; set; }
        public int? ThemeId { get; set; }
        //TODO: список оцінок
    }
}
