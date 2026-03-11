using MedMonitor24_7.DTOs.Profile;

namespace MedMonitor24_7.Services.Interfaces
{
    public interface IProfileService
    {
        Task<ProfileResponseDto?> GetCurrentProfileAsync(int userId);
        Task<bool> UpdateCurrentProfileAsync(int userId, UpdateProfileDto dto);
    }
}