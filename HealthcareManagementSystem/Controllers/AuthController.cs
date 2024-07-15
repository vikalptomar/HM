using HealthcareManagementSystem.Authorization;
using HealthcareManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HealthcareManagementSystem.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public IAuthService _AuthService;
        public IJwtUtils _JwtUtils;
        public AuthController(IAuthService authService, IJwtUtils jwtUtils)
        {
            _AuthService = authService;
            _JwtUtils = jwtUtils;
        }
        // GET: api/<AuthController>

        [HttpGet("Login")]
        public async Task<IActionResult> Login(string userEmail, string password)
        {
            var currentUser = await _AuthService.Login(userEmail, password);
            if (currentUser.UserId == 0)
            {
                return Ok(new { status = 404, message = "Not Found" });
                //return NotFound();
            }
            else
            {
                var token = _JwtUtils.GenerateJwtToken(currentUser);
                return Ok(new { status = 200, token });
            }
        }
    }
}
