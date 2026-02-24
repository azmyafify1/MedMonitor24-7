using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MedMonitor24_7.Models
{
   
        [Index(nameof(Identifier), IsUnique = true)]
        [Index(nameof(BedID))]
        public class Device
        {
            [Key]
            public int DeviceID { get; set; }

            [Required]
            public int BedID { get; set; }

            [Required, MaxLength(30)]
            public string DeviceType { get; set; } = null!; // Camera, SpO2, ECG ...

            [Required, MaxLength(100)]
            public string Identifier { get; set; } = null!; // Serial/MAC/Topic

            [Required]
            public DeviceStatus Status { get; set; } = DeviceStatus.Online;

            public DateTime? LastSeenAt { get; set; }

            public Bed Bed { get; set; } = null!;
            public ICollection<DeviceHeartbeat> Heartbeats { get; set; } = new List<DeviceHeartbeat>();
        }
}
