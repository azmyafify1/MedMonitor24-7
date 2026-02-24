using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MedMonitor24_7.Models;

[Index(nameof(Name))]
public class Unit
{
    [Key]
    public int UnitID { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    public int MaxBeds { get; set; }

    // Navigation
    public ICollection<Bed> Beds { get; set; } = new List<Bed>();
    public ICollection<UserUnit> UserUnits { get; set; } = new List<UserUnit>();
}