using System.ComponentModel.DataAnnotations;
using PractiFly.WebApi.Attributes;

namespace PractiFly.WebApi.Dto.Profile
{
    public class UserProfileInfoCreateDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(128)]
        public string LastName { get; set; } = null!;

        [Phone]
        [MaxLength(32)]
        public string? PhoneNumber { get; set; }

        [EmailAddress]
        [MaxLength(64)]
        public string? Email { get; set; }
        
        [TodayDateConstraint]
        public DateOnly Birthday { get; set; }

        [Url]
        [MaxLength(2048)]
        public string? FilePhoto { get; set; }
        
        [RoleString]
        public string Role { get; set; } = null!;
    }
}
