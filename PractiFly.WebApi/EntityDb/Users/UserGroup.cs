using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PractiFly.WebApi.EntityDb.Users
{
    [Table("UserGroup")]
    public class UserGroup
    {
        [Column("UserId")]
        public int UserId { get; set; }

        [Column("GroupId")]
        public int GroupId { get; set; }

        [Column("IsActive")]
        public bool IsActive { get; set; }

        [Column("Note")]
        [MaxLength(256)]
        public string? Note { get; set; }
    }
}
