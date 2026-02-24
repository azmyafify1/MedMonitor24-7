using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace MedMonitor24_7.Models
{
    [Index(nameof(AdmissionID), nameof(VitalSignTypeID), nameof(ReadingTime))]
    public class VitalSignReading
    {
        [Key]
        public int ReadingID { get; set; }

        [Required]
        public int AdmissionID { get; set; }

        [Required]
        public int VitalSignTypeID { get; set; }

        public float Value { get; set; }

        public DateTime ReadingTime { get; set; }

        [Required]
        public VitalSource Source { get; set; } = VitalSource.Sensor;

        public bool IsAbnormal { get; set; }

        public Admission Admission { get; set; } = null!;
        public VitalSignType VitalSignType { get; set; } = null!;
    }
}
