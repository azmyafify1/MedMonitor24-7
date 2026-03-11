using System.Security.Claims;
using MedMonitor24_7.DTOs.AdmissionRequests;
using MedMonitor24_7.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedMonitor24_7.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdmissionRequestsController : ControllerBase
    {
        private readonly IAdmissionRequestService _admissionRequestService;

        public AdmissionRequestsController(IAdmissionRequestService admissionRequestService)
        {
            _admissionRequestService = admissionRequestService;
        }

        [HttpPost]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> Create([FromBody] CreateAdmissionRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdClaim, out int doctorId))
                return Unauthorized(new { message = "Invalid token" });

            var result = await _admissionRequestService.CreateAsync(doctorId, dto);
            return Ok(result);
        }

        [HttpGet("pending")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> GetPending()
        {
            var result = await _admissionRequestService.GetPendingAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _admissionRequestService.GetByIdAsync(id);

            if (result == null)
                return NotFound(new { message = "Request not found" });

            return Ok(result);
        }

        [HttpPut("{id}/review")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Review(int id, [FromBody] ReviewAdmissionRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdClaim, out int adminId))
                return Unauthorized(new { message = "Invalid token" });

            var reviewed = await _admissionRequestService.ReviewAsync(id, adminId, dto);

            if (!reviewed)
                return BadRequest(new { message = "Review failed. Request may not exist, may already be reviewed, or decision is invalid." });

            return Ok(new { message = "Request reviewed successfully" });
        }
    }
}