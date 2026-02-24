using System.ComponentModel.DataAnnotations;

namespace MedMonitor24_7.Models
{
    public class NotificationSettings
    {
        [Key]
        public int UserID { get; set; }

        public bool EnableNotifications { get; set; }
        public bool HeartRateAlert { get; set; }
        public bool OxygenAlert { get; set; }
        public bool TaskDelayAlert { get; set; }

        public SystemUser User { get; set; } = null!;
    }
}
