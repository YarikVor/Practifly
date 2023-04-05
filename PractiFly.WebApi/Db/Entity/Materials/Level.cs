using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace PractiFly.WebApi.EntityDb.Materials;
/*
 *	Id
        	Name
        	Number
        	Note
        	Description
 * 
 */



[Table("Level")]
[PrimaryKey("Id")]
public class Level
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }

    [Column("Name")]
    [MaxLength(256)]
    [Required]
    [MaybeNull]
    public string Name { get; set; }

    [Column("Number")]
    [ForeignKey("Number")]
    [Required]
    public int Number { get; set; }

    [Column("Note")]
    [MaxLength(256)]
    public string? Note { get; set; }

    [Column("Description")]
    [MaxLength(65536)]
    public string? Description { get; set; }
}