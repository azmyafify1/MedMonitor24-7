using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MedMonitor24_7.Models
{

    [Index(nameof(Email), IsUnique = true)]
    public class SystemUser
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        public int RoleID { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required, EmailAddress, MaxLength(255)]
        public string Email { get; set; } = null!;

        [Required, MaxLength(300)]
        public string PasswordHash { get; set; } = null!;

        [Phone, MaxLength(20)]
        public string? Phone { get; set; }

        public DateTime? DOB { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [MaxLength(100)]
        public string? Specialization { get; set; }

        [Required]
        public UserStatus Status { get; set; } = UserStatus.Active;

        public DateTime CreatedAt { get; set; }

        // Navigation
        public Role Role { get; set; } = null!;

        public ICollection<Admission> DoctorAdmissions { get; set; } = new List<Admission>();
        public ICollection<Admission> NurseAdmissions { get; set; } = new List<Admission>();

        public ICollection<PatientReport> UploadedReports { get; set; } = new List<PatientReport>();

        public ICollection<ClinicalTask> CreatedTasks { get; set; } = new List<ClinicalTask>();
        public ICollection<ClinicalTask> AssignedTasks { get; set; } = new List<ClinicalTask>();
        public ICollection<TaskEvent> TaskEvents { get; set; } = new List<TaskEvent>();

        public ICollection<UserUnit> UserUnits { get; set; } = new List<UserUnit>();

        public NotificationSettings? NotificationSettings { get; set; }

        public ICollection<ChatMessage> SentMessages { get; set; } = new List<ChatMessage>();
        public ICollection<ChatMessage> ReceivedMessages { get; set; } = new List<ChatMessage>();

        public ICollection<AlertAcknowledgement> AlertAcknowledgements { get; set; } = new List<AlertAcknowledgement>();
    }
}
