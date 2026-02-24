using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MedMonitor24_7.Models
{
    [Index(nameof(AdmissionID), nameof(VitalSignTypeID), IsUnique = true)]
    public class AdmissionVitalThreshold
    {
        [Key]
        public int ThresholdID { get; set; }

        [Required]
        public int AdmissionID { get; set; }

        [Required]
        public int VitalSignTypeID { get; set; }

        public float? MinValue { get; set; }
        public float? MaxValue { get; set; }

        [Required]
        public AlertSeverity Severity { get; set; } = AlertSeverity.Warning;

        public Admission Admission { get; set; } = null!;
        public VitalSignType VitalSignType { get; set; } = null!;
    }
}
