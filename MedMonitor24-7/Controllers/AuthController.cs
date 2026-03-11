using MedMonitor24_7.DTOs.Auth;
using MedMonitor24_7.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MedMonitor24_7.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.LoginAsync(dto);

            if (result == null)
                return Unauthorized(new { message = "Invalid email or password" });

            return Ok(result);
        }
    }
}