using MedMonitor24_7.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace YourNamespace.Models;

public class SystemUser
{
    [Key]
    public int UserID { get; set; }

    [Required]
    public int RoleID { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    [Required]
    [EmailAddress]
    [MaxLength(255)]
    public string Email { get; set; } = null!;

    [Required]
    public string PasswordHash { get; set; } = null!;

    [Phone]
    [MaxLength(20)]
    public string? Phone { get; set; }

    public DateTime? DOB { get; set; }

    [Required]
    [MaxLength(20)]
    public string Gender { get; set; } = null!;

    [MaxLength(100)]
    public string? Specialization { get; set; }

    [Required]
    [MaxLength(20)]
    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    // Navigation
    public Role Role { get; set; } = null!;
    public ICollection<Admission> DoctorAdmissions { get; set; } = new List<Admission>();
    public ICollection<Admission> NurseAdmissions { get; set; } = new List<Admission>();
    public ICollection<PatientReport> UploadedReports { get; set; } = new List<PatientReport>();
    public ICollection<ClinicalTask> CreatedTasks { get; set; } = new List<ClinicalTask>();

    public ICollection<ClinicalTask> AssignedTasks { get; set; } = new List<ClinicalTask>();

    // Admission Requests
    public ICollection<AdmissionRequest> RequestedAdmissionRequests { get; set; } = new List<AdmissionRequest>();
    public ICollection<AdmissionRequest> ReviewedAdmissionRequests { get; set; } = new List<AdmissionRequest>();

    public NotificationSettings NotificationSettings { get; set; } = null!;
    public ICollection<UserUnit> UserUnits { get; set; } = new List<UserUnit>();
    public ICollection<ChatMessage> SentMessages { get; set; } = new List<ChatMessage>();
    public ICollection<ChatMessage> ReceivedMessages { get; set; } = new List<ChatMessage>();
}