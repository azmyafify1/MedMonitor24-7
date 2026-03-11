using System.ComponentModel.DataAnnotations;

namespace MedMonitor24_7.DTOs.AdmissionRequests
{
    public class CreateAdmissionRequestDto
    {
        [Required]
        [MaxLength(200)]
        public string PatientName { get; set; } = null!;

        [MaxLength(50)]
        public string? PatientIdentifier { get; set; }

        [Required]
        [Range(0, 150)]
        public int Age { get; set; }

        [Required]
        [MaxLength(20)]
        public string Gender { get; set; } = null!;

        public float Height { get; set; }

        public float Weight { get; set; }

        [Phone]
        [MaxLength(20)]
        public string? CompanionPhone { get; set; }

        [MaxLength(1000)]
        public string? Diagnosis { get; set; }
    }
}