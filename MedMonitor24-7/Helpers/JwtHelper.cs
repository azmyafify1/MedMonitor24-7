using MedMonitor24_7.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using YourNamespace.Models;

namespace MedMonitor24_7.Helpers
{
    public class JwtHelper
    {
        private readonly IConfiguration _configuration;

        public JwtHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public (string token, DateTime expiresAt) GenerateToken(SystemUser user, string roleName)
        {
            var jwtSection = _configuration.GetSection("Jwt");

            var key = jwtSection["Key"]!;
            var issuer = jwtSection["Issuer"]!;
            var audience = jwtSection["Audience"]!;
            var durationInMinutes = int.Parse(jwtSection["DurationInMinutes"]!);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserID.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, roleName)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var expiresAt = DateTime.UtcNow.AddMinutes(durationInMinutes);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expiresAt,
                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return (tokenString, expiresAt);
        }
    }
}