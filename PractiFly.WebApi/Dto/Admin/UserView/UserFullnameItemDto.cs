using System.ComponentModel.DataAnnotations;

namespace PractiFly.WebApi.Dto.Admin.UserView
{
    public class UserFullnameItemDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string Fullname { get; set; } = null!;
    }
}
