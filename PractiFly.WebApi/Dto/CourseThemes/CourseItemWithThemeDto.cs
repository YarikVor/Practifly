namespace PractiFly.WebApi.Dto.CourseThemes
{
    //Сторінка: "Теми курсів????"
    public class CourseItemWithThemeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ThemeItemDto[] Themes { get; set; } = null!;
    }
}
