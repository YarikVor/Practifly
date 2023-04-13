//Сторінка "Результати навчання" у Figma
//DTO курсів
using System.ComponentModel.DataAnnotations;

namespace PractiFly.WebApi.Dto.StudyResults
{
    public class StudyResultsCoursesDto
    {
        [Required]
        public int CourseId { get; set; }
        
        //TODO: Check here.
        public string Course { get; set; } = null!;
    }
}
