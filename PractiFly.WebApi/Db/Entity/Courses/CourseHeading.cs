using Microsoft.EntityFrameworkCore;
using PractiFly.WebApi.EntityDb.Materials;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PractiFly.WebApi.EntityDb.Courses
{
    [Table("CourseHeading")]
    [Keyless]
    public class CourseHeading
    {
        [Column("CourseId")]
        public int CourseId { get; set; }

        [ForeignKey("CourseId")]
        public virtual Course Course { get; set; } = null!; //TODO:

        [Column("HeadingId")]
        public int HeadingId { get; set; }

        [ForeignKey("HeadingId")]
        public virtual Heading Heading { get; set; } = null!; //TODO:

        [Column("IsBasic")]
        [Required]
        public bool IsBasic { get; set; }

        [Column("Note")]
        [MaxLength(256)]
        public string? Note { get; set; }
    }
}
