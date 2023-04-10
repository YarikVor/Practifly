using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PractiFly.WebApi.EntityDb.Users;

[Table("Group")]
[PrimaryKey("Id")]
public class Group
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }

    [Column("Name")]
    [MaxLength(256)]
    [Required]
    public string Name { get; set; } = null!;

    [Column("FoundationDate")]
    public DateOnly FoundationDate { get; set; }

    [Column("TerminationDate")]
    public DateOnly TerminationDate { get; set; }

    [Column("Note")]
    [MaxLength(256)]
    public string? Note { get; set; }
}