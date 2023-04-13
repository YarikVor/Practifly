using System.ComponentModel.DataAnnotations;

namespace PractiFly.WebApi.Dto.CourseDetails
{
    public class CourseDetailsMenuDto
    {
        [Required]
        public int ThemeId { get; set; }

        [Required]
        public string Theme { get; set; } = null!;
        
        [Required]
        public int MaterialId { get; set; }
        
        [Required]
        public string Material { get; set; } = null!;

        //TODO: Check here. Чи треба тут ця властивість?
        public string StatusFlag { get; set; } = null!;

    }
}
