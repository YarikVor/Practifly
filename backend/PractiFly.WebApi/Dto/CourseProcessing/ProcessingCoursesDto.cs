//Сторінка "Проходження курсів" у Figma
//DTO курсів

using System.ComponentModel.DataAnnotations;

namespace PractiFly.WebApi.Dto.StudyResults;

public class ProcessingCoursesDto
{
    [Required] public int Id { get; set; }

    public string Course { get; set; } = null!;
}