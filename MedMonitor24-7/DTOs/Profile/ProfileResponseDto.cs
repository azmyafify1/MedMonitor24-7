namespace MedMonitor24_7.DTOs.Profile
{
    public class ProfileResponseDto
    {
        public int UserID { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public DateTime? DOB { get; set; }
        public string Gender { get; set; } = null!;
        public string? Specialization { get; set; }
        public string Status { get; set; } = null!;
        public string Role { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}