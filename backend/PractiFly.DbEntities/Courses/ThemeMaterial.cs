﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PractiFly.DbEntities.Materials;

namespace PractiFly.DbEntities.Courses;

[Table("ThemeMaterial")]
[PrimaryKey("Id")]
public class ThemeMaterial
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }

    [Column("ThemeId")]
    public int ThemeId { get; set; }

    [ForeignKey("ThemeId")]
    public virtual Theme Theme { get; set; } = null!;

    [Column("MaterialId")]
    public int MaterialId { get; set; }

    [ForeignKey("MaterialId")]
    public virtual Material Material { get; set; } = null!;

    [Column("Number")]
    [Required]
    public int Number { get; set; }

    [Column("IsBasic")]
    [Required]
    public bool IsBasic { get; set; }

    [Column("LevelId")]
    public int LevelId { get; set; }

    [ForeignKey("LevelId")]
    public virtual Level Level { get; set; } = null!;

    [Column("Note")]
    [MaxLength(EntitiesConstantLengths.Note)]
    public string? Note { get; set; }

    [Column("Description")]
    [MaxLength(65536)]
    public string? Description { get; set; }
}