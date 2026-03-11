using System.ComponentModel.DataAnnotations;
using YourNamespace.Models;

namespace MedMonitor24_7.Models
{
    public class Unit
    {
        [Key]
        public int UnitID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        public int MaxBeds { get; set; }

        public ICollection<Bed> Beds { get; set; } = new List<Bed>();
        public ICollection<UserUnit> UserUnits { get; set; } = new List<UserUnit>();
    }
}