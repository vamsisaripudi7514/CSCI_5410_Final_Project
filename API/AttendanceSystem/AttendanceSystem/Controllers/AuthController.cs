using AttendanceSystem.DTOs.Auth;
using AttendanceSystem.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly AttendanceSystemContext _context;
        public AuthController(AttendanceSystemContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> UserLogin([FromBody] LoginRequestPayload data) {
            if (data == null || string.IsNullOrWhiteSpace(data.Username) || string.IsNullOrWhiteSpace(data.Password))
            {
                return BadRequest("Invalid login request.");
            }
            var user = _context.Users.FirstOrDefault(u => u.Username == data.Username);
            if (user == null)
            {
                return NotFound("User Not Found.");
            }

            if(user.PasswordHash != data.Password)
            {
                return Unauthorized("Invalid Credentials");
            }
            var master_user = _context.Usermasters.FirstOrDefault(u => u.MasterId == user.UserId);
            var response = new LoginResponsePayload
            {
                UserId = user.UserId,
                Username = user.Username,
                Role = master_user.Role,
                JWTtoken = "Sample_token" // Need to fill this for JWT
            };
            return Ok(response);
        }
    }
}
