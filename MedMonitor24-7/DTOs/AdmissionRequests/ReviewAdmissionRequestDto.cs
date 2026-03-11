using System.ComponentModel.DataAnnotations;

namespace MedMonitor24_7.DTOs.AdmissionRequests
{
    public class ReviewAdmissionRequestDto
    {
        [Required]
        [MaxLength(20)]
        public string Decision { get; set; } = null!; // Approved or Rejected

        [MaxLength(1000)]
        public string? ReviewNote { get; set; }
    }
}