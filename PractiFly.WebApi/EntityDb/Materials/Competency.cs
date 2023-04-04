using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PractiFly.WebApi.EntityDb.Materials;


[Table("Competency")]
public class Competency
{
	[Column("Id")]
    public int Id { get; set; }
    
    [Column("Name")]
    public string Name { get; set; }
    
    
    [ForeignKey("HeadingId")]
    [Column("HeadingId")]
    public int HeadingId { get; set; }
    
    public virtual Heading Heading { get; set; }
    
    [Column("ParentId")]
    public int ParentId { get; set; }
    
    [Column("Note")]
    public string Note { get; set; }
    
    [Column("Description")]
    public string Description { get; set; }
    
}