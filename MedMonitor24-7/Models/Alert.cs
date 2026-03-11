using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using YourNamespace.Models;

namespace MedMonitor24_7.Models
{
    [Index(nameof(AdmissionID), nameof(Status), nameof(RaisedAt))]
    public class Alert
    {
        [Key]
        public int AlertID { get; set; }

        [Required]
        public int AdmissionID { get; set; }

        [Required]
        public int VitalSignTypeID { get; set; }

        public float Value { get; set; }

        [Required]
        public AlertSeverity Severity { get; set; } = AlertSeverity.Warning;

        [Required]
        public AlertStatus Status { get; set; } = AlertStatus.Active;

        public DateTime RaisedAt { get; set; }

        [MaxLength(300)]
        public string? Message { get; set; }

        public Admission Admission { get; set; } = null!;
        public VitalSignType VitalSignType { get; set; } = null!;

        public ICollection<AlertAcknowledgement> Acknowledgements { get; set; } = new List<AlertAcknowledgement>();
    }
}
