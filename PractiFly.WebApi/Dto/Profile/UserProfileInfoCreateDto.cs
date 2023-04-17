using System.ComponentModel.DataAnnotations;

namespace PractiFly.WebApi.Dto.Profile
{
    public class UserProfileInfoCreateDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(128)]
        public string Surname { get; set; } = null!;

        [Phone]
        [MaxLength(32)]
        public string? Phone { get; set; }

        [EmailAddress]
        [MaxLength(64)]
        public string? Email { get; set; }

        public DateOnly Birthday { get; set; }

        [Url]
        [MaxLength(2048)]
        public string? FilePhoto { get; set; }
    }
}
