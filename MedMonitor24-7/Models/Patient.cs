using MedMonitor24_7.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace YourNamespace.Models;

public class Patient
{
    [Key]
    public int PatientID { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = null!;

    [MaxLength(50)]
    public string? PatientIdentifier { get; set; }

    [Required]
    public int Age { get; set; }

    [Required]
    [MaxLength(20)]
    public string Gender { get; set; } = null!;

    public float Height { get; set; }

    public float Weight { get; set; }

    [Phone]
    [MaxLength(20)]
    public string? CompanionPhone { get; set; }

    [MaxLength(1000)]
    public string? Diagnosis { get; set; }

    public DateTime CreatedAt { get; set; }

    // Navigation
    public ICollection<Admission> Admissions { get; set; } = new List<Admission>();
}