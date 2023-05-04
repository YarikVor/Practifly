using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PractiFly.DbEntities.Users;

[Table("Group")]
[PrimaryKey("Id")]
public class Group
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }

    [Column("Name")]
    [MaxLength(EntitiesConstantLengths.Name)]
    [Required]
    public string Name { get; set; } = null!;

    [Column("FoundationDate")]
    public DateOnly FoundationDate { get; set; }

    [Column("TerminationDate")]
    public DateOnly TerminationDate { get; set; }

    [Column("Note")]
    [MaxLength(EntitiesConstantLengths.Note)]
    public string? Note { get; set; }
}