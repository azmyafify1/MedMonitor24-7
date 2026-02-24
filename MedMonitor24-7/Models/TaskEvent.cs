using System.ComponentModel.DataAnnotations;

namespace MedMonitor24_7.Models
{
    public class TaskEvent
    {
        [Key]
        public int EventID { get; set; }

        [Required]
        public int TaskID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public TaskEventType EventType { get; set; }

        public DateTime EventAt { get; set; }

        [MaxLength(300)]
        public string? Note { get; set; }

        public ClinicalTask Task { get; set; } = null!;
        public SystemUser User { get; set; } = null!;
    }
}
