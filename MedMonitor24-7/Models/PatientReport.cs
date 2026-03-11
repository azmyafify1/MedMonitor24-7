using System.ComponentModel.DataAnnotations;
using YourNamespace.Models;

namespace MedMonitor24_7.Models
{
    public class PatientReport
    {
        [Key]
        public int ReportID { get; set; }

        [Required]
        public int AdmissionID { get; set; }

        [Required]
        public int CategoryID { get; set; }

        [Required]
        public int UploadedByUserID { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }

        public DateTime ReportDate { get; set; }

        public Admission Admission { get; set; } = null!;
        public ReportCategory Category { get; set; } = null!;
        public SystemUser UploadedByUser { get; set; } = null!;

        public ICollection<ReportFile> Files { get; set; } = new List<ReportFile>();
    }
}
