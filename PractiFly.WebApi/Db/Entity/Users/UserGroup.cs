using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PractiFly.WebApi.EntityDb.Users
{
    [Table("UserGroup")]
    [Keyless]
    public class UserGroup
    {
        [Column("UserId")]
        public int UserId { get; set; }
        
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        

        [Column("GroupId")]
        public int GroupId { get; set; }

        [Column("IsActive")]
        public bool IsActive { get; set; }

        [Column("Note")]
        [MaxLength(256)]
        public string? Note { get; set; }
    }
}
