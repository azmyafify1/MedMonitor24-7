using System.ComponentModel.DataAnnotations;

namespace MedMonitor24_7.DTOs.Profile
{
    public class UpdateProfileDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; } = null!;

        [Phone]
        [MaxLength(20)]
        public string? Phone { get; set; }

        public DateTime? DOB { get; set; }

        [Required]
        [MaxLength(20)]
        public string Gender { get; set; } = null!;

        [MaxLength(100)]
        public string? Specialization { get; set; }
    }
}