using System.ComponentModel.DataAnnotations.Schema;
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


[PrimaryKey("Id")]
[Table("Level")]
public class Level
{
    [Column("Id")]
    public 
}