using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MedMonitor24_7.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class VitalSignType
    {
        [Key]
        public int VitalSignTypeID { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = null!;

        [MaxLength(50)]
        public string? Unit { get; set; }

        public float NormalMin { get; set; }
        public float NormalMax { get; set; }

        public ICollection<VitalSignReading> VitalSignReadings { get; set; } = new List<VitalSignReading>();
        public ICollection<AdmissionVitalThreshold> AdmissionThresholds { get; set; } = new List<AdmissionVitalThreshold>();
        public ICollection<Alert> Alerts { get; set; } = new List<Alert>();
    }
}
