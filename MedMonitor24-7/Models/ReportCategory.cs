using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MedMonitor24_7.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class ReportCategory
    {
        [Key]
        public int CategoryID { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; } = null!;

        public ICollection<PatientReport> PatientReports { get; set; } = new List<PatientReport>();
    }
}
