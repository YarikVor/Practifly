using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PractiFly.DbEntities.Materials;
using PractiFly.DbEntities.Users;

namespace PractiFly.DbEntities.Courses;

[Table("Course")]
[PrimaryKey("Id")]
public class Course
{
    [Key]
    [Column("Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("Name")]
    [MaxLength(EntitiesConstantLengths.Name)]
    [Required]
    public string Name { get; set; } = null!;

    [ForeignKey("AspNetUsers")]
    [Column("OwnerId")]

    public int OwnerId { get; set; }

    [ForeignKey("OwnerId")]
    public virtual User Owner { get; set; } = null!;

    [Column("LanguageId")]
    [ForeignKey("Language")]
    public int LanguageId { get; set; }

    [ForeignKey("LanguageId")]
    public virtual Language Language { get; set; } = null!;

    [Column("Note")]
    [MaxLength(EntitiesConstantLengths.Note)]
    public string? Note { get; set; }

    [Column("Description")]
    [MaxLength(EntitiesConstantLengths.Description)]
    public string? Description { get; set; }
}