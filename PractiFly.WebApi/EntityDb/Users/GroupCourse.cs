using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PractiFly.WebApi.EntityDb.Users
{
    [Table("GroupCourse")]
    public class GroupCourse
    {
        [Column("GroupId")]
        public int GroupId { get; set; }

        [Column("CourseId")]
        public int CourseId { get; set; }

        [Column("LevelId")]
        public int LevelId { get; set; }

        [Column("IsCompleted")]
        public bool IsCompleted { get; set; }

        [Column("Note")]
        [MaxLength(256)]
        public string? Note { get; set; }

    }
}
