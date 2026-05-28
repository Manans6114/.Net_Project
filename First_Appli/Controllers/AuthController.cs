using First_Appli.Common.DTOs;
using First_Appli.Service.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace First_Appli.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // =========================================
        // REGISTER API
        // =========================================
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response =
                await _authService.Register(request);

            return Ok(response);
        }

        // =========================================
        // LOGIN API
        // =========================================
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response =
                await _authService.Login(request);

            if (!response.IsSuccess)
            {
                return Unauthorized(response);
            }

            return Ok(response);
        }
    }
}