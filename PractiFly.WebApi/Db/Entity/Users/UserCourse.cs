using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PractiFly.WebApi.EntityDb.Users
{
    [Table("UserCourse")]
    public class UserCourse
    {
        [Column("UserId")]
        public int UserId { get; set; }
        
        [Column("CourseId")]
        public int CourseId { get; set; }

        [Column("LevelId")]
        public int LevelId { get; set; }

        [Column("IsCompleted")]
        public bool IsCompleted { get; set; }

        //[Timestamp]
        [Column("LastTime")]
        public TimeOnly LastTime { get; set; }

        [Column("LastThemeId")]
        public int LastThemeId { get; set; }

        [Column("Grade")]
        public int Grade { get; set; }

        [MaxLength(256)]
        [Column("Note")]
        public string? Note { get; set; }
    }
}
