using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MedMonitor24_7.Models
{
    [Index(nameof(AssignedNurseID), nameof(Status), nameof(StartDateTime))]
    public class ClinicalTask
    {
        [Key]
        public int TaskID { get; set; }

        [Required]
        public int AdmissionID { get; set; }

        [Required]
        public int CreatedByUserID { get; set; }

        [Required]
        public int AssignedNurseID { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; } = null!;

        [MaxLength(1000)]
        public string? Note { get; set; }

        [Required]
        public DateTime StartDateTime { get; set; }

        public DateTime? EndDateTime { get; set; }

        [MaxLength(100)]
        public string? Reminder { get; set; }

        [MaxLength(100)]
        public string? RepeatRule { get; set; }

        [MaxLength(50)]
        public string? Color { get; set; }

        [Required]
        public TaskStatus Status { get; set; } = TaskStatus.Open;

        public Admission Admission { get; set; } = null!;
        public SystemUser CreatedByUser { get; set; } = null!;
        public SystemUser AssignedNurse { get; set; } = null!;

        public ICollection<TaskEvent> Events { get; set; } = new List<TaskEvent>();
    }
}
