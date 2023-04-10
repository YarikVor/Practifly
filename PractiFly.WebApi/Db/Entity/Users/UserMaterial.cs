using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PractiFly.WebApi.EntityDb.Materials;

namespace PractiFly.WebApi.EntityDb.Users;

[Table("UserMaterial")]
[PrimaryKey("Id")]
public class UserMaterial
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }

    [Column("UserId")]
    public int UserId { get; set; }

    [ForeignKey("UserId")]
    public virtual User User { get; set; } = null!;

    [Column("MaterialId")]
    public int MaterialId { get; set; }

    [ForeignKey("MaterialId")]
    public virtual Material Material { get; set; } = null!;

    [Column("IsCompleted")]
    [Required]
    public bool IsCompleted { get; set; }

    [Column("ResultUrl")]
    [Url]
    [Required]
    [MaxLength(256)]
    public string ResultUrl { get; set; } = null!;

    [Column("Grade")]
    [Required]
    public int Grade { get; set; }

    [Column("Note")]
    [MaxLength(256)]
    public string? Note { get; set; }
}