using HealthcareManagementSystem.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HealthcareManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public IAuthService _AuthService;
        public AuthController(IAuthService authService) 
        {
            _AuthService = authService;
        }
        // GET: api/<AuthController>
        [HttpGet("Login")]
        public async Task<IActionResult> Login(string useremail, string password)
        {
            var currentUser = await _AuthService.Login(useremail, password);
            if (currentUser.UserId == 0) 
            { 
                return Ok(new{ status=404, message="Not Found"});
               //return NotFound();
            }

            return Ok(new{ status=200, currentUser});
        }
    }
}
