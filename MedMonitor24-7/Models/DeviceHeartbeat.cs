using System.ComponentModel.DataAnnotations;

namespace MedMonitor24_7.Models
{
    public class DeviceHeartbeat
    {
        [Key]
        public int HeartbeatID { get; set; }

        [Required]
        public int DeviceID { get; set; }

        public DateTime SeenAt { get; set; }

        [MaxLength(400)]
        public string? Meta { get; set; }

        public Device Device { get; set; } = null!;
    }
}
