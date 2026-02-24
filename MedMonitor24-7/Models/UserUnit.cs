using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedMonitor24_7.Models
{
    [Table("User_Unit")]
    public class UserUnit
    {
        public int UserID { get; set; }
        public int UnitID { get; set; }

        [MaxLength(50)]
        public string? RoleInUnit { get; set; }

        public SystemUser User { get; set; } = null!;
        public Unit Unit { get; set; } = null!;
    }
}
