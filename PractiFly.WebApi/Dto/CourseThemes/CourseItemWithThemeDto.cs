namespace PractiFly.WebApi.Dto.CourseThemes
{
    //Сторінка: "Теми курсів????"
    public class CourseItemWithThemeDto
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; } = null!;
        public int ThemeId { get; set; }
        public string ThemeName { get; set; } = null!;

        //TODO: Меню матеріалів.
    }
}
