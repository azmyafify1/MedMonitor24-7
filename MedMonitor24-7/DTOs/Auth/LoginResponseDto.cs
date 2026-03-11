namespace MedMonitor24_7.DTOs.Auth
{
    public class LoginResponseDto
    {
        public int UserID { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string Token { get; set; } = null!;
        public DateTime ExpiresAt { get; set; }
    }
}