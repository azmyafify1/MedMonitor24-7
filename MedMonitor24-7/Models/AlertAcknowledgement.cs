using System.ComponentModel.DataAnnotations;
using YourNamespace.Models;

namespace MedMonitor24_7.Models
{
    public class AlertAcknowledgement
    {
        [Key]
        public int AckID { get; set; }

        [Required]
        public int AlertID { get; set; }

        [Required]
        public int UserID { get; set; }

        public DateTime AckAt { get; set; }

        [MaxLength(300)]
        public string? Note { get; set; }

        public Alert Alert { get; set; } = null!;
        public SystemUser User { get; set; } = null!;
    }
}
