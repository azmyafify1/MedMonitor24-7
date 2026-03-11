using System.Security.Claims;
using MedMonitor24_7.DTOs.Profile;
using MedMonitor24_7.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedMonitor24_7.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMyProfile()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdClaim, out int userId))
                return Unauthorized(new { message = "Invalid token" });

            var profile = await _profileService.GetCurrentProfileAsync(userId);

            if (profile == null)
                return NotFound(new { message = "User not found" });

            return Ok(profile);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMyProfile([FromBody] UpdateProfileDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdClaim, out int userId))
                return Unauthorized(new { message = "Invalid token" });

            var updated = await _profileService.UpdateCurrentProfileAsync(userId, dto);

            if (!updated)
                return BadRequest(new { message = "Update failed. Email may already exist or user not found." });

            return Ok(new { message = "Profile updated successfully" });
        }
    }
}