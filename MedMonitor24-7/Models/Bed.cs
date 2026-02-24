using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MedMonitor24_7.Models
{
    
        [Index(nameof(UnitID), nameof(BedCode), IsUnique = true)]
        public class Bed
        {
            [Key]
            public int BedID { get; set; }

            [Required]
            public int UnitID { get; set; }

            [Required, MaxLength(20)]
            public string BedCode { get; set; } = null!;

            [Required]
            public BedStatus Status { get; set; } = BedStatus.Available;

            public Unit Unit { get; set; } = null!;

            public ICollection<Admission> Admissions { get; set; } = new List<Admission>();
            public ICollection<Device> Devices { get; set; } = new List<Device>();
        }
    }

