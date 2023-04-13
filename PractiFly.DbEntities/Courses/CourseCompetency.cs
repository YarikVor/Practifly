﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PractiFly.DbEntities.Materials;

namespace PractiFly.DbEntities.Courses;

[Table("CourseCompetency")]
[PrimaryKey("Id")]
public class CourseCompetency
{
    [Key]
    [Column("Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("CourseId")]
    public int CourseId { get; set; }

    [ForeignKey("CourseId")]
    public virtual Course Course { get; set; } = null!;

    [Column("CompetencyId")]
    public int CompetencyId { get; set; }

    [ForeignKey("CompetencyId")]
    public virtual Competency Competency { get; set; } = null!;

    [Column("Note")]
    [MaxLength(256)]
    public string? Note { get; set; }
}