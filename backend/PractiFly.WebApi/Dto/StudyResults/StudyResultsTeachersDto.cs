using System.ComponentModel.DataAnnotations;

namespace PractiFly.WebApi.Dto.StudyResults;

public class StudyResultsTeachersDto
{
    [Required] public int TeacherId { get; set; }

    public string Teacher { get; set; } = null!;
}