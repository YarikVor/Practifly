using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace PractiFly.WebApi.EntityDb.Users
{
    [Table("UserMaterial")]
    public class UserMaterial
    {
        [Column("UserId")]
        public int UserId { get; set; }

        [Column("MaterialId")]
        public int MaterialId { get; set; }
        
        [Column("IsCompleted")]
        public bool IsCompleted { get; set; }
        
        [Column("ResultUrl")]
        [Url]
        [MaxLength(256)]
        [MaybeNull]
        public string ResultUrl { get; set; }
        
        [Column("Grade")]
        public int Grade { get; set; }
        
        [Column("Note")]
        [MaxLength(256)]
        public string? Note { get; set; } 
    }
}
