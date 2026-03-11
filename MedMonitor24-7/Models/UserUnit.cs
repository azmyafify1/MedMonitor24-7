using MedMonitor24_7.Models;
using System.ComponentModel.DataAnnotations;

namespace YourNamespace.Models;

public class UserUnit
{
    public int UserID { get; set; }

    public int UnitID { get; set; }

    [MaxLength(50)]
    public string? RoleInUnit { get; set; }

    // Navigation
    public SystemUser User { get; set; } = null!;
    public Unit Unit { get; set; } = null!;
}