using MedMonitor24_7.DTOs.AdmissionRequests;

namespace MedMonitor24_7.Services.Interfaces
{
    public interface IAdmissionRequestService
    {
        Task<AdmissionRequestResponseDto> CreateAsync(int doctorId, CreateAdmissionRequestDto dto);
        Task<List<AdmissionRequestResponseDto>> GetPendingAsync();
        Task<AdmissionRequestResponseDto?> GetByIdAsync(int requestId);
        Task<bool> ReviewAsync(int requestId, int adminId, ReviewAdmissionRequestDto dto);
    }
}