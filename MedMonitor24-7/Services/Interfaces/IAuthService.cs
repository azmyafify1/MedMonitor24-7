using MedMonitor24_7.DTOs.Auth;

namespace MedMonitor24_7.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto?> LoginAsync(LoginRequestDto dto);
    }
}