//Сторінка "Дані курсів" у Figma

namespace PractiFly.WebApi.Dto.CourseData
{
    public class CourseItemDto
    {
        public int Id { get; set; }

        //TODO: Check here.
        public string Name { get; set; } = null!;
    }
}
