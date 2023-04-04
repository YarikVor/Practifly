<<<<<<< HEAD
=======
using Microsoft.EntityFrameworkCore.Metadata.Internal;
>>>>>>> 5f83ebe5a27e4fd456fb84303ffca9069855d10d
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace PractiFly.WebApi.EntityDb.Materials;


[Table("Competency")]
public class Competency
{
<<<<<<< HEAD
	[Column("Id")]
    public int Id { get; set; }
    
    [Column("Name")]
=======
    [Key]
    [Column("Id")]
    public int Id { get; set; }

    [Column("Name")]
    [MaxLength(256)]
    [Required]
    [MaybeNull]
>>>>>>> 5f83ebe5a27e4fd456fb84303ffca9069855d10d
    public string Name { get; set; }

    [Column("HeadingId")]
    [ForeignKey("HeadingId")]
<<<<<<< HEAD
    [Column("HeadingId")]
=======
    [Required]
>>>>>>> 5f83ebe5a27e4fd456fb84303ffca9069855d10d
    public int HeadingId { get; set; }
    
    public virtual Heading Heading { get; set; }
    
    [Column("ParentId")]
<<<<<<< HEAD
    public int ParentId { get; set; }
    
    [Column("Note")]
    public string Note { get; set; }
    
    [Column("Description")]
    public string Description { get; set; }
=======
    [ForeignKey("ParentId")]
    public int ParentId { get; set; }

    public virtual Competency id { get; set; } //TODO:

    [Column("Note")]
    [MaxLength(256)]
    public string? Note { get; set; }

    [Column("Description")]
    [MaxLength(65536)]
    public string? Description { get; set; }
>>>>>>> 5f83ebe5a27e4fd456fb84303ffca9069855d10d
    
}