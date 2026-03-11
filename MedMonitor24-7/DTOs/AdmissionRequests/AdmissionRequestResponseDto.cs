namespace MedMonitor24_7.DTOs.AdmissionRequests
{
    public class AdmissionRequestResponseDto
    {
        public int RequestID { get; set; }
        public int RequestedByDoctorID { get; set; }
        public string RequestedByDoctorName { get; set; } = null!;
        public string PatientName { get; set; } = null!;
        public string? PatientIdentifier { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; } = null!;
        public float Height { get; set; }
        public float Weight { get; set; }
        public string? CompanionPhone { get; set; }
        public string? Diagnosis { get; set; }
        public string Status { get; set; } = null!;
        public DateTime RequestedAt { get; set; }
        public int? ReviewedByAdminID { get; set; }
        public string? ReviewedByAdminName { get; set; }
        public DateTime? ReviewedAt { get; set; }
        public string? ReviewNote { get; set; }
    }
}