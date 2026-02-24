using System.ComponentModel.DataAnnotations;

namespace MedMonitor24_7.Models
{
    public class ReportFile
    {
        [Key]
        public int FileID { get; set; }

        [Required]
        public int ReportID { get; set; }

        [Required, MaxLength(255)]
        public string FileName { get; set; } = null!;

        [Required, MaxLength(1000)]
        public string FilePath { get; set; } = null!;

        public DateTime UploadedAt { get; set; }

        public PatientReport Report { get; set; } = null!;
    }
}
