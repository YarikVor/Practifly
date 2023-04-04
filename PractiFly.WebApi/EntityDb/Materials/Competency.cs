using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace PractiFly.WebApi.EntityDb.Materials;
/*Id
        	Name
	HeadingId 
        	ParentId // в тому числі вид компетентності
        	Note
        	Description*/

[Table("Competency")]
public class Competency
{
    [Key]
    [Column("Id")]
    public int Id { get; set; }

    [Column("Name")]
    [MaxLength(256)]
    [Required]
    [MaybeNull]
    public string Name { get; set; }

    [Column("HeadingId")]
    [ForeignKey("HeadingId")]
    [Required]
    public int HeadingId { get; set; }
    
    public virtual Heading Heading { get; set; }
    
    [Column("ParentId")]
    [ForeignKey("ParentId")]
    public int ParentId { get; set; }

    public virtual Competency id { get; set; } //TODO:

    [Column("Note")]
    [MaxLength(256)]
    public string? Note { get; set; }

    [Column("Description")]
    [MaxLength(65536)]
    public string? Description { get; set; }
    
}