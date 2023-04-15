using System.ComponentModel.DataAnnotations;

namespace PractiFly.WebApi.Dto.Admin.UserView
{
    public class UserFilteringDto
    {
        [MaxLength(128)]
        public string? Name { get; set; }
        
        [MaxLength(128)]
        public string? Surname { get; set; }

        [Phone]
        [MaxLength(32)]
        public string? Phone { get; set; } 

        public DateOnly? RegistrationDateFrom { get; set; }
        public DateOnly? RegistrationDateTo { get; set; }

        [EmailAddress]
        [MaxLength(128)]
        public string? Email { get; set; }

        //TODO: Ролі користувачів (може змінитись тип даних)
        public string? Role { get; set; }
    }
}
