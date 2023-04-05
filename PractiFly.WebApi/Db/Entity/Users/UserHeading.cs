using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using PractiFly.WebApi.EntityDb.Materials;

namespace PractiFly.WebApi.EntityDb.Users
{
    [Table("UserHeading")]
    [Keyless]
    public class UserHeading
    {
        [Column("UserId")] 
        public int UserId { get; set; }

        [ForeignKey("UserId")] 
        public virtual User User { get; set; } = null!;

        [Column("HeadingId")] 
        public int HeadingId { get; set; }

        [ForeignKey("HeadingId")] 
        public virtual Heading Heading { get; set; } = null!;

        [Column("LevelId")] 
        public int LevelId { get; set; }

        [ForeignKey("LevelId")] 
        public virtual Level Level { get; set; } = null!;

        [Column("Note")] 
        [MaxLength(256)] 
        public string? Note { get; set; }
    }
}