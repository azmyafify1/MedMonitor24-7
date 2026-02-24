using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MedMonitor24_7.Models
{
    [Index(nameof(BedID))]
    [Index(nameof(PatientID))]
    public class Admission
    {
        [Key]
        public int AdmissionID { get; set; }

        [Required]
        public int PatientID { get; set; }

        [Required]
        public int BedID { get; set; }

        [Required]
        public int DoctorID { get; set; }

        [Required]
        public int NurseID { get; set; }

        [Required]
        public DateTime AdmitDate { get; set; }

        public DateTime? DischargeDate { get; set; }

        [Required]
        public AdmissionStatus Status { get; set; } = AdmissionStatus.Active;

        // Navigation
        public Patient Patient { get; set; } = null!;
        public Bed Bed { get; set; } = null!;
        public SystemUser Doctor { get; set; } = null!;
        public SystemUser Nurse { get; set; } = null!;

        public ICollection<VitalSignReading> VitalSignReadings { get; set; } = new List<VitalSignReading>();
        public ICollection<PatientReport> PatientReports { get; set; } = new List<PatientReport>();
        public ICollection<ClinicalTask> ClinicalTasks { get; set; } = new List<ClinicalTask>();

        // Added parts
        public ICollection<Alert> Alerts { get; set; } = new List<Alert>();
        public ICollection<AdmissionVitalThreshold> VitalThresholds { get; set; } = new List<AdmissionVitalThreshold>();
    }
}
