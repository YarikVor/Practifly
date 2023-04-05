using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using PractiFly.WebApi.EntityDb.Courses;
using PractiFly.WebApi.EntityDb.Materials;

namespace PractiFly.WebApi.EntityDb.Users
{
    [Table("GroupCourse")]
    [Keyless]
    public class GroupCourse
    {
        [Column("GroupId")]
        public int GroupId { get; set; }
        
        [ForeignKey("GroupId")]
        public virtual Group Group { get; set; }

        [Column("CourseId")]
        public int CourseId { get; set; }
        
        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; }
        
        [Column("LevelId")]
        public int LevelId { get; set; }
        
        [ForeignKey("LevelId")]
        public virtual Level Level { get; set; }

        [Column("IsCompleted")]
        public bool IsCompleted { get; set; }

        [Column("Note")]
        [MaxLength(256)]
        public string? Note { get; set; }

    }
}
