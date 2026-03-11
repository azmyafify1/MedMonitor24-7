using MedMonitor24_7.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace YourNamespace.Models;

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

    public int? NurseID { get; set; }

    [Required]
    public DateTime AdmitDate { get; set; }

    public DateTime? DischargeDate { get; set; }

    [Required]
    [MaxLength(50)]
    public string Status { get; set; } = null!;

    // Navigation
    public Patient Patient { get; set; } = null!;
    public Bed Bed { get; set; } = null!;
    public SystemUser Doctor { get; set; } = null!;
    public SystemUser? Nurse { get; set; }

    public ICollection<VitalSignReading> VitalSignReadings { get; set; } = new List<VitalSignReading>();
    public ICollection<PatientReport> PatientReports { get; set; } = new List<PatientReport>();
    public ICollection<ClinicalTask> ClinicalTasks { get; set; } = new List<ClinicalTask>();
}