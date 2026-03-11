using MedMonitor24_7.DataAccess;
using MedMonitor24_7.DTOs.Profile;
using MedMonitor24_7.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedMonitor24_7.Services.Implementations
{
    public class ProfileService : IProfileService
    {
        private readonly ApplicationDbContext _context;

        public ProfileService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProfileResponseDto?> GetCurrentProfileAsync(int userId)
        {
            var user = await _context.SystemUsers
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserID == userId);

            if (user == null)
                return null;

            return new ProfileResponseDto
            {
                UserID = user.UserID,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                DOB = user.DOB,
                Gender = user.Gender,
                Specialization = user.Specialization,
                Status = user.Status,
                Role = user.Role.Name,
                CreatedAt = user.CreatedAt
            };
        }

        public async Task<bool> UpdateCurrentProfileAsync(int userId, UpdateProfileDto dto)
        {
            var user = await _context.SystemUsers
                .FirstOrDefaultAsync(u => u.UserID == userId);

            if (user == null)
                return false;

            var emailExists = await _context.SystemUsers
                .AnyAsync(u => u.Email == dto.Email && u.UserID != userId);

            if (emailExists)
                return false;

            user.Name = dto.Name;
            user.Email = dto.Email;
            user.Phone = dto.Phone;
            user.DOB = dto.DOB;
            user.Gender = dto.Gender;
            user.Specialization = dto.Specialization;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}