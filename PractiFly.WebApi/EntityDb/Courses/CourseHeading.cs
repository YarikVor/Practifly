using PractiFly.WebApi.EntityDb.Materials;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PractiFly.WebApi.EntityDb.Courses
{
    [Table("CourseHeading")]
    public class CourseHeading
    {
        [Column("CourseId")]
        [ForeignKey("CourseId")]
        [Required]
        public int CourseId { get; set; }
        public virtual Course Course { get; set; } //TODO:

        [Column("HeadingId")]
        [ForeignKey("HeadingId")]
        [Required]
        public int HeadingId { get; set; }
        public virtual Heading Heading { get; set; } //TODO:


        [Column("IsBasic")]
        [ForeignKey("IsBasic")]
        [Required]
        public bool IsBasic { get; set; }

        [Column("Note")]
        [MaxLength(256)]
        public string? Note { get; set; }
    }
}
