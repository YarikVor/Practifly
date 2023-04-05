using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using PractiFly.WebApi.EntityDb.Materials;

namespace PractiFly.WebApi.EntityDb.Users
{
    [Table("UserMaterial")]
    [Keyless]
    public class UserMaterial
    {
        [Column("UserId")]
        public int UserId { get; set; }
        
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [Column("MaterialId")]
        public int MaterialId { get; set; }
        
        [ForeignKey("MaterialId")]
        public virtual Material Material { get; set; }
        
        [Column("IsCompleted")]
        public bool IsCompleted { get; set; }

        [Column("ResultUrl")]
        [Url]
        [MaxLength(256)]
        public string ResultUrl { get; set; } = null!;
        
        [Column("Grade")]
        public int Grade { get; set; }
        
        [Column("Note")]
        [MaxLength(256)]
        public string? Note { get; set; } 
    }
}
