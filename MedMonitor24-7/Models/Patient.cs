using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MedMonitor24_7.Models
{
    [Index(nameof(Name))]
    public class Patient
    {
        [Key]
        public int PatientID { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; } = null!;

        public int Age { get; set; }

        [Required]
        public Gender Gender { get; set; }

        public float Height { get; set; }
        public float Weight { get; set; }

        [Phone, MaxLength(20)]
        public string? CompanionPhone { get; set; }

        [MaxLength(1000)]
        public string? Diagnosis { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<Admission> Admissions { get; set; } = new List<Admission>();
    }
}
