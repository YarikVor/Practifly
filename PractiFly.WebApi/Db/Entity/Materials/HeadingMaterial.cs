using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PractiFly.WebApi.EntityDb.Materials;

[Table("HeadingMaterial")]
[PrimaryKey("Id")]
public class HeadingMaterial
{    
    [Key]
    [Column("Id")]
    public int Id { get; set; }
    
    [Column("HeadingId")]
    public int HeadingId { get; set; }

    [ForeignKey("HeadingId")]
    public virtual Heading Heading { get; set; } = null!;

    [Column("MaterialId")]
    public int MaterialId { get; set; }

    [ForeignKey("MaterialId")]
    public virtual Material Material { get; set; } = null!;

    [Column("Note")]
    [MaxLength(256)]
    public string? Note { get; set; }
}

