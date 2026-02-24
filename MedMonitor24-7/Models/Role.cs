using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MedMonitor24_7.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Role
    {
        [Key]
        public int RoleID { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; } = null!;

        public ICollection<SystemUser> Users { get; set; } = new List<SystemUser>();
    }
}
