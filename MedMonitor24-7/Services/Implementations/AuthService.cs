using MedMonitor24_7.DataAccess;
using MedMonitor24_7.DTOs.Auth;
using MedMonitor24_7.Helpers;
using MedMonitor24_7.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MedMonitor24_7.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtHelper _jwtHelper;

        public AuthService(ApplicationDbContext context, JwtHelper jwtHelper)
        {
            _context = context;
            _jwtHelper = jwtHelper;
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto dto)
        {
            var user = await _context.SystemUsers
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null)
                return null;

            if (user.PasswordHash != dto.Password)
                return null;

            var roleName = user.Role.Name;
            var (token, expiresAt) = _jwtHelper.GenerateToken(user, roleName);

            return new LoginResponseDto
            {
                UserID = user.UserID,
                Name = user.Name,
                Email = user.Email,
                Role = roleName,
                Token = token,
                ExpiresAt = expiresAt
            };
        }
    }
}