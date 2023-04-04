using System.ComponentModel.DataAnnotations.Schema;

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
	
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    
    [ForeignKey("HeadingId")]
    public int HeadingId { get; set; }
    
    public virtual Heading Heading { get; set; }
    
    public int ParentId { get; set; }
    
    public string Note { get; set; }
    
    public string Description { get; set; }
    
}