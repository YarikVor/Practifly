using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PractiFly.WebApi.EntityDb.Users
{
    [Table("UserTheme")]
    public class UserTheme
    {
        [Column("UserId")]
        public int UserId { get; set; }

        [Column("ThemeId")]
        public int ThemeId { get; set; }

        [Column("IsCompleted")]
        public bool IsCompleted { get; set; }

        [Column("LevelId")]
        public int LevelId { get; set; }

        [Column("Grade")]
        public int Grade { get; set; }

        [Column("Note")]
        [MaxLength(256)]
        public string? Note { get; set; }
    }
}
