using System;
using System.ComponentModel.DataAnnotations;

namespace YourNamespace.Models;

public class AdmissionRequest
{
    [Key]
    public int RequestID { get; set; }

    [Required]
    public int RequestedByDoctorID { get; set; }

    [Required]
    [MaxLength(200)]
    public string PatientName { get; set; } = null!;

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

    [Required]
    [MaxLength(50)]
    public string Status { get; set; } = "Pending";

    public DateTime RequestedAt { get; set; }

    public int? ReviewedByAdminID { get; set; }

    public DateTime? ReviewedAt { get; set; }

    [MaxLength(1000)]
    public string? ReviewNote { get; set; }

    // Navigation
    public SystemUser RequestedByDoctor { get; set; } = null!;
    public SystemUser? ReviewedByAdmin { get; set; }
}