using Microsoft.EntityFrameworkCore;
using PractiFly.WebApi.EntityDb.Materials;
using PractiFly.WebApi.EntityDb.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PractiFly.WebApi.EntityDb.Courses;

[Table("CourseCompetency")]
public class CourseCompetency
{
    [Column("CourseId")]
    [ForeignKey("CourseId")]
    [Required]
    public int CourseId { get; set; }
    public virtual Course Course { get; set; } //TODO:

    [Column("CompetencyId")]
    [ForeignKey("CompetencyId")]
    [Required]

    public int CompetencyId{ get; set; }
    public virtual Competency Competency { get; set; } //TODO:

    [Column("Note")]
    [MaxLength(256)]
    public string? Note { get; set; }

}
