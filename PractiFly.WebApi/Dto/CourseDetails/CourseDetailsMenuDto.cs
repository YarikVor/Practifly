using System.ComponentModel.DataAnnotations;

namespace PractiFly.WebApi.Dto.CourseDetails
{
    public class CourseDetailsMenuDto
    {
        public int ThemeId { get; set; }

        public string Theme { get; set; } = null!;
        
        public int MaterialId { get; set; }
        
        public string Material { get; set; } = null!;

        [Required]
        public bool IsSelected { get; set; }

        //TODO: Check here. Чи треба тут ця властивість?
        
        //public string StatusFlag { get; set; } = null!;

    }
}
