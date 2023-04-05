using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace PractiFly.WebApi.EntityDb.Users
{
    [Table("UserHeading")]
    //[PrimaryKey("UserId")]
    public class UserHeading
    {
        [Key]
        [Column("UserId")]
        public int UserId { get; set; }
        [Column("HeadingId")]
        [MaybeNull]
        public int HeadingId { get; set; }
        [Column("LevelId")]
        [MaybeNull]
        public int LevelId { get; set; }
        [Column("Note")]
        [MaxLength(256)]
        public string? Note { get; set; }
    }
}
