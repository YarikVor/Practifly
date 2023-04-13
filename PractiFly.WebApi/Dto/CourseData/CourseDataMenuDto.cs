//Сторінка "Дані курсів" у Figma

using System.ComponentModel.DataAnnotations;

namespace PractiFly.WebApi.Dto.CourseData
{
    public class CourseDataMenuDto
    {
        [Required]
        public int CourseId { get; set; }

        //TODO: Check here.
        public string Course { get; set; } = null!;
    }
}
