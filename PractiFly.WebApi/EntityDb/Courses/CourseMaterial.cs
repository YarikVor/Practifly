using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PractiFly.WebApi.EntityDb.Courses
{
    [Table("CourseMaterial")]
    public class CourseMaterial
    {
        [Column("CourseId")]
        [ForeignKey("CourseId")]
        [Required]
        public int CourseId { get; set; }
        public virtual Course Course { get; set; } //TODO:

        [Column("MaterialId")]
        [ForeignKey("MaterialId")]
        [Required]
        public int MaterialId { get; set; }
        public virtual Material Material { get; set; } //TODO:

        [Column("PriorityLevel")]
        [ForeignKey("PriorityLevel")]
        [Required]
        public int PriorityLevel { get; set; }

        [Column("IsReserved")]
        [ForeignKey("IsReserved")]
        [Required]
        public bool IsReserved { get; set; }

        [Column("Note")]
        [MaxLength(256)]
        public string? Note { get; set; }
    }
}
