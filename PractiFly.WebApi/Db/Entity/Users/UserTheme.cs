using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PractiFly.WebApi.EntityDb.Courses;
using PractiFly.WebApi.EntityDb.Materials;

namespace PractiFly.WebApi.EntityDb.Users
{
    [Table("UserTheme")]
    [Keyless]
    public class UserTheme
    {
        [Column("UserId")]
        public int UserId { get; set; }
        
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [Column("ThemeId")]
        public int ThemeId { get; set; }
        
        [ForeignKey("ThemeId")]
        public virtual Theme Theme { get; set; }

        [Column("IsCompleted")]
        public bool IsCompleted { get; set; }

        [Column("LevelId")]
        public int LevelId { get; set; }
        
        [ForeignKey("LevelId")]
        public virtual Level Level { get; set; }

        [Column("Grade")]
        public int Grade { get; set; }

        [Column("Note")]
        [MaxLength(256)]
        public string? Note { get; set; }
    }
}
