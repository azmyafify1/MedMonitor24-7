using MedMonitor24_7.DataAccess;
using MedMonitor24_7.DTOs.AdmissionRequests;
using MedMonitor24_7.Models;
using MedMonitor24_7.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using YourNamespace.Models;

namespace MedMonitor24_7.Services.Implementations
{
    public class AdmissionRequestService : IAdmissionRequestService
    {
        private readonly ApplicationDbContext _context;

        public AdmissionRequestService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<AdmissionRequestResponseDto> CreateAsync(int doctorId, CreateAdmissionRequestDto dto)
        {
            var doctor = await _context.SystemUsers
                .FirstOrDefaultAsync(u => u.UserID == doctorId);

            if (doctor == null)
                throw new Exception("Doctor not found.");

            var request = new AdmissionRequest
            {
                RequestedByDoctorID = doctorId,
                PatientName = dto.PatientName,
                PatientIdentifier = dto.PatientIdentifier,
                Age = dto.Age,
                Gender = dto.Gender,
                Height = dto.Height,
                Weight = dto.Weight,
                CompanionPhone = dto.CompanionPhone,
                Diagnosis = dto.Diagnosis,
                Status = "Pending",
                RequestedAt = DateTime.UtcNow
            };

            _context.AdmissionRequests.Add(request);
            await _context.SaveChangesAsync();

            return new AdmissionRequestResponseDto
            {
                RequestID = request.RequestID,
                RequestedByDoctorID = request.RequestedByDoctorID,
                RequestedByDoctorName = doctor.Name,
                PatientName = request.PatientName,
                PatientIdentifier = request.PatientIdentifier,
                Age = request.Age,
                Gender = request.Gender,
                Height = request.Height,
                Weight = request.Weight,
                CompanionPhone = request.CompanionPhone,
                Diagnosis = request.Diagnosis,
                Status = request.Status,
                RequestedAt = request.RequestedAt,
                ReviewedByAdminID = request.ReviewedByAdminID,
                ReviewedByAdminName = null,
                ReviewedAt = request.ReviewedAt,
                ReviewNote = request.ReviewNote
            };
        }

        public async Task<List<AdmissionRequestResponseDto>> GetPendingAsync()
        {
            return await _context.AdmissionRequests
                .Include(r => r.RequestedByDoctor)
                .Include(r => r.ReviewedByAdmin)
                .Where(r => r.Status == "Pending")
                .OrderByDescending(r => r.RequestedAt)
                .Select(r => new AdmissionRequestResponseDto
                {
                    RequestID = r.RequestID,
                    RequestedByDoctorID = r.RequestedByDoctorID,
                    RequestedByDoctorName = r.RequestedByDoctor.Name,
                    PatientName = r.PatientName,
                    PatientIdentifier = r.PatientIdentifier,
                    Age = r.Age,
                    Gender = r.Gender,
                    Height = r.Height,
                    Weight = r.Weight,
                    CompanionPhone = r.CompanionPhone,
                    Diagnosis = r.Diagnosis,
                    Status = r.Status,
                    RequestedAt = r.RequestedAt,
                    ReviewedByAdminID = r.ReviewedByAdminID,
                    ReviewedByAdminName = r.ReviewedByAdmin != null ? r.ReviewedByAdmin.Name : null,
                    ReviewedAt = r.ReviewedAt,
                    ReviewNote = r.ReviewNote
                })
                .ToListAsync();
        }

        public async Task<AdmissionRequestResponseDto?> GetByIdAsync(int requestId)
        {
            return await _context.AdmissionRequests
                .Include(r => r.RequestedByDoctor)
                .Include(r => r.ReviewedByAdmin)
                .Where(r => r.RequestID == requestId)
                .Select(r => new AdmissionRequestResponseDto
                {
                    RequestID = r.RequestID,
                    RequestedByDoctorID = r.RequestedByDoctorID,
                    RequestedByDoctorName = r.RequestedByDoctor.Name,
                    PatientName = r.PatientName,
                    PatientIdentifier = r.PatientIdentifier,
                    Age = r.Age,
                    Gender = r.Gender,
                    Height = r.Height,
                    Weight = r.Weight,
                    CompanionPhone = r.CompanionPhone,
                    Diagnosis = r.Diagnosis,
                    Status = r.Status,
                    RequestedAt = r.RequestedAt,
                    ReviewedByAdminID = r.ReviewedByAdminID,
                    ReviewedByAdminName = r.ReviewedByAdmin != null ? r.ReviewedByAdmin.Name : null,
                    ReviewedAt = r.ReviewedAt,
                    ReviewNote = r.ReviewNote
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> ReviewAsync(int requestId, int adminId, ReviewAdmissionRequestDto dto)
        {
            var request = await _context.AdmissionRequests
                .FirstOrDefaultAsync(r => r.RequestID == requestId);

            if (request == null)
                return false;

            if (request.Status != "Pending")
                return false;

            var decision = dto.Decision.Trim();

            if (decision != "Approved" && decision != "Rejected")
                return false;

            request.Status = decision;
            request.ReviewedByAdminID = adminId;
            request.ReviewedAt = DateTime.UtcNow;
            request.ReviewNote = dto.ReviewNote;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}