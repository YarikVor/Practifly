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
    [Required]
    [MaybeNull]
	[DataType(DataType.Text)]
	public string Code { get; set; }
	
	[Column("Name")]
	[Required]
	[MaybeNull]
	[DataType(DataType.Text)]
	public string Name { get; set; }
	
	[Column("UDC")]
    [Required]
    [MaybeNull]
	[DataType(DataType.Text)]
	public string Udc { get; set; }
	
	[Column("Note")]
	[DataType(DataType.Text)]
	public string? Note { get; set; }
	
	[Column("Description")]
	[DataType(DataType.Text)]
	public string? Description { get; set; }

}