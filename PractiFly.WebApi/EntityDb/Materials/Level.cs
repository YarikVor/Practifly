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
    
}