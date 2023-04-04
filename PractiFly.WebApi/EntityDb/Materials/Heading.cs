using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PractiFly.WebApi.EntityDb.Materials;

[Table("Heading")]
[PrimaryKey("Id")]
public class Heading
{
	[Column("Id")]
	public int Id { get; set; }
	
	[Column("Code")]
	[MaybeNull]
	public string Code { get; set; }
	
	[Column("Name")]
	[MaybeNull]
	public string Name { get; set; }
	
	[Column("UDC")]
	[MaybeNull]
	public string Udc { get; set; }
	
	[Column("Note")]
	public string? Note { get; set; }
	
	[Column("Description")]
	public string? Description { get; set; }

}