using Microsoft.EntityFrameworkCore;
using PractiFly.WebApi.EntityDb.Materials;
using PractiFly.WebApi.EntityDb.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PractiFly.WebApi.EntityDb.Courses;

[Table("CourseCompetency")]
[Keyless]
public class CourseCompetency
{
    [Column("CourseId")]
    public int CourseId { get; set; }

    [ForeignKey("CourseId")]
    public virtual Course Course { get; set; } = null!; //TODO:

    [Column("CompetencyId")]
    public int CompetencyId{ get; set; }

    [ForeignKey("CompetencyId")]
    public virtual Competency Competency { get; set; } = null!; //TODO:

    [Column("Note")]
    [MaxLength(256)]
    public string? Note { get; set; }

}
